using System.Diagnostics;
using System.Windows.Input;
using Rcn.Common;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        public AuthViewModel()
        {
            AuthCommand = new Command(AuthMethod);
            Debug.WriteLine($"AuthViewModel: ctor");
            Title = "Добро пожаловать";
        }

        private async void AuthMethod()
        {
            var user = await DataStore.Auth(PersonnelNumber);
            if (user == null)
            {
                await AppShell.Alert("Вход не удался",
                    "Введен неверный либо несуществующий табельный номер.",
                    null, "ОК");
                return;
            }

            Globals.CurrentUser = user;
            AppShell.GoToPage(new EnterpriseSelectorViewModel());

        }

        public string PersonnelNumber
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public ICommand AuthCommand { get; set; }
    }
}
