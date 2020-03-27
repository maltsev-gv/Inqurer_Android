using System.Windows.Input;
using Rcn.Common;
using Rcn.Common.ExtensionMethods;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public RegistrationViewModel()
        {
            Title = "Добро пожаловать";
            RegisterCommand = new Command(RegisterMethod);
        }

        private async void RegisterMethod()
        {
            if (Password.IsNullOrWhiteSpace())
            {
                var registerAnyway = await AppShell.Alert("Не указан логин/пароль",
                    "Вы ввели только PIN-код. Уверены, что хотите продолжить?",
                    "Да", "Нет");
                if (!registerAnyway)
                {
                    return;
                }
            }

            var user =  DataStore.Auth(Login, Password, Pin);
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

        public string Pin
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public ICommand RegisterCommand { get; set; }

        //public object BackCommand
        //{
        //    get { throw new System.NotImplementedException(); }
        //}
    }
}
