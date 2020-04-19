using System;
using System.Collections.Generic;
using System.Text;
using InquirerForAndroid.Models;
using InquirerForAndroid.ViewModels;
using Xamarin.Forms;

namespace InquirerForAndroid
{
    public class Globals
    {
        public static string BaseUrl = "http://84.201.162.94:45000/api/inquirer/";

        public static Page ActivePage
        {
            get => _activePage;
            set
            {
                _activePage = value;
                ActivePageChanged?.Invoke();
            }
        }

        public static UserInfo CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                CurrentUserChanged?.Invoke();
            }
        }

        public static EnterpriseInfo CurrentEnterprise
        {
            get => _currentEnterprise;
            set => _currentEnterprise = value;
        }

        public static ViewModelBase CurrentViewModel { get; set; }

        public static Action ActivePageChanged;
        public static Action CurrentUserChanged;

        private static UserInfo _currentUser;
        private static Page _activePage;
        private static EnterpriseInfo _currentEnterprise;
    }
}
