
namespace Legasy2.Core.Service
{
    public static class PromtService
    {
        public static async Task<CaseClass> AddCase(string _number)
        {
            string title = _number;
            string _qualification = await Shell.Current.DisplayPromptAsync(title, $"Введіть кваліфікацію:", maxLength: 3);
            int _qualificationInt = 0;
            if (!int.TryParse(_qualification, out _qualificationInt) | String.IsNullOrWhiteSpace(_qualification))
            {
                return null;
            }

            string _header = await Shell.Current.DisplayPromptAsync(title, $"Введіть заголовок:", maxLength: 20);

            return new CaseClass
            {
                CriminalNumber = _number,
                Header = _header,
                Qualification = _qualification,
            };
        }

        public static async Task<CaseClass> EditCase(CaseClass caseClass)
        {
            string title = caseClass.CriminalNumber;
            string _qualification = await Shell.Current.DisplayPromptAsync(title, $"Введіть кваліфікацію (3 цифри):", initialValue: caseClass.Qualification, maxLength: 3);
            int _qualificationInt = 0;
            if (!int.TryParse(_qualification, out _qualificationInt) | String.IsNullOrWhiteSpace(_qualification))
            {
                return null;
            }

            string _header = await Shell.Current.DisplayPromptAsync(title, $"Введіть заголовок:", initialValue: caseClass.Header, maxLength: 20);

            return new CaseClass
            {
                N = caseClass.N,
                CriminalNumber = caseClass.CriminalNumber,
                Header = _header,
                Qualification = _qualification,
            };
        }

        public static async Task<bool> DeleteCase(CaseClass caseClass)
        {
            string title = caseClass.CriminalNumber;
            bool _ansver = await Shell.Current.DisplayAlert(title, $"Видалити {caseClass.Header}", "Так", "Ні");
            if (_ansver)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
