using System;
using System.Collections.ObjectModel;
using System.Security.Authentication;
using System.Threading.Tasks;
using InquirerForAndroid.Models;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        public AppShellViewModel()
        {
            GotToPageCommand = new Command(GotToPageMethod);
            Auth();
        }

        private void GotToPageMethod(object viewModel)
        {
            AppShell.GoToPage((ViewModelBase) viewModel);
        }

        public Command GotToPageCommand { get; }

        public async Task Auth()
        {
            try
            {
                await DataStore.Auth("");
            }
            catch (AuthenticationException)
            { }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
