

namespace Legasy2.Core.View;

public partial class MainPage : ContentPage
{
	MainViewModel mainViewModel;
	public MainPage()
	{
		InitializeComponent();
		mainViewModel= new MainViewModel();
		BindingContext= mainViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        mainViewModel.OnAppearing();
    }
}