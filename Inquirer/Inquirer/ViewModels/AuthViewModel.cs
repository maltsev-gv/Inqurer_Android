using System.Windows.Input;
using Rcn.Common;
using Xamarin.Forms;

namespace Inquirer_Android.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        public AuthViewModel()
        {
            //Title = "Авторизация";
            AuthCommand = new Command(AuthMethod);
        }

        private void AuthMethod()
        {
            
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
