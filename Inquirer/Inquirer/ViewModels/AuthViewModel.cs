using System.Windows.Input;
using Rcn.Common;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        public AuthViewModel()
        {
            Title = "Добро пожаловать";
            AuthCommand = new Command(AuthMethod);
        }

        private async void AuthMethod()
        {
            var user = DataStore.Auth(Login, Password, "");
            if (user == null)
            {
                await AppShell.Alert("Вход не удался",
                    "Введен неверный PIN-код, либо данный PIN-код уже не действителен.",
                    null, "ОК");
                return;
            }

            Globals.CurrentUser = user;
            AppShell.GoToPage(new EnterpriseSelectorViewModel());

        }

        public string Login
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public string Password
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public ICommand AuthCommand { get; set; }
    }
}
