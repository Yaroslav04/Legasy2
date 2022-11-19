using Legasy2.Core.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Legasy2.Core.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            ItemTappedDouble = new Command<CaseClass>(OnDoubleItemTapped);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SearchCommand = new Command(Search);
            ClearCommand = new Command(Clear);
            EditCommand = new Command(Edit);
            DeleteCommand = new Command(Delete);
            Items = new ObservableCollection<CaseClass>();
            QualificationsList = new ObservableCollection<string>();

        }
        public void OnAppearing()
        {
            UpdateQualification();
            IsBusy = true;
            SelectedItem = null;
        }

        public async void UpdateQualification()
        {
            QualificationsList.Clear();
            var q = await App.DataBase.Case.GetQualificationsAsync();
            if (q.Count > 0)
            {
                foreach (var item in q)
                {
                    QualificationsList.Add(item);
                }
            }
        }

        public ObservableCollection<CaseClass> Items { get; }

        private CaseClass selectedItem;
        public CaseClass SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
            }
        }

        public ObservableCollection<string> QualificationsList { get; }

        private string selectedQualification;
        public string SelectedQualification
        {
            get => selectedQualification;
            set
            {
                SetProperty(ref selectedQualification, value);
            }
        }

        public Command<CaseClass> ItemTappedDouble { get; }
        public Command LoadItemsCommand { get; }
        public Command SearchCommand { get; }
        public Command ClearCommand { get; }
        public Command EditCommand { get; }
        public Command DeleteCommand { get; }

        private void OnDoubleItemTapped(CaseClass item)
        {
            if (item == null)
            {
                return;
            }
            FileManager.OpenFolder(item.CriminalNumber);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                if (!String.IsNullOrWhiteSpace(SearchText))
                {
                    if (TextService.IsNumberValid(SearchText))
                    {

                        if (!await App.DataBase.Case.IsCaseExist(SearchText))
                        {
                            var cs = await PromtService.AddCase(SearchText);
                            if (cs != null)
                            {
                                try
                                {
                                    await App.DataBase.Case.SaveAsync(cs);
                                    FileManager.CreateNewDirectory(cs.CriminalNumber);
                                    FileManager.OpenFolder(cs.CriminalNumber);
                                    UpdateQualification();
                                    Clear();
                                    return;
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }

                Items.Clear();
                var c = await App.DataBase.Case.GetListAsync();
                if (c.Count > 0)
                {
                    //SearchText
                    if (!String.IsNullOrWhiteSpace(SearchText))
                    {
                        if (TextService.IsNumberValid(SearchText))
                        {
                            if (await App.DataBase.Case.IsCaseExist(SearchText))
                            {
                                c = c.Where(x => x.CriminalNumber == SearchText).ToList();
                            }
                        }
                        else
                        {
                            c = c.Where(x => x.Header.Contains(SearchText)).ToList();
                        }
                    }

                    //SearchQualification
                    if (SelectedQualification != null)
                    {
                        if (c.Count > 0)
                        {
                            c = c.Where(x => x.Qualification == SelectedQualification).ToList();
                        }
                    }

                    if (c.Count > 0)
                    {
                        foreach (var item in c)
                        {
                            Items.Add(item);
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Search()
        {
            IsBusy = true;
        }

        private void Clear()
        {
            SearchText = null;
            SelectedQualification = null;
            IsBusy = true;
        }

        private async void Edit()
        {
            if (SelectedItem != null)
            {
                try
                {
                    var cs = await PromtService.EditCase(SelectedItem);
                    if (cs != null) 
                    {
                        await App.DataBase.Case.UpdateAsync(cs);
                    }
                    Clear();
                }
                catch
                {

                }
            }
        }

        private async void Delete()
        {
            if (SelectedItem != null)
            {
                if (await PromtService.DeleteCase(SelectedItem))
                {
                    try
                    {
                        try
                        {
                            FileManager.DeleteDirectory(SelectedItem.CriminalNumber);
                        }
                        catch
                        {

                        }

                        await App.DataBase.Case.DeleteAsync(SelectedItem);
                        Clear();
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
