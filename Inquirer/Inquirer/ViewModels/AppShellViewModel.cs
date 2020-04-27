﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using InquirerForAndroid.Models;
using InquirerForAndroid.Services;
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
                var user = await DataStore.Auth("1234");
                if (user != null)
                {
                    var enterprises = await DataStore.GetEnterprises(true);
                    Globals.CurrentEnterpriseId = enterprises.GetEnterprisesByFilter(null, true).FirstOrDefault()?.EnterpriseId ?? 0;
                }
            }
            catch (AuthenticationException)
            { 
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
