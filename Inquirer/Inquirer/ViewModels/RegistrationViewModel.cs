using System.Windows.Input;
using Rcn.Common;
using Xamarin.Forms;

namespace Inquirer_Android.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public RegistrationViewModel()
        {
            //Title = "Регистрация";
            RegisterCommand = new Command(RegisterMethod);
        }

        private void RegisterMethod()
        {
            
        }

        public string Login
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public string Password1
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public string Password2
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
    }
}
