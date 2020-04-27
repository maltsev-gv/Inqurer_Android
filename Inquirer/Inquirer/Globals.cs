using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                _activePage.Appearing -= OnAppearing;
                _activePage.Appearing += OnAppearing;
                ActivePageChanged?.Invoke();
            }
        }

        private static void OnAppearing(object sender, EventArgs e)
        {
            var vm = (ViewModelBase) _activePage.BindingContext;
            Debug.WriteLine($"OnAppearing: {vm.GetType().Name} - {vm.Title}");
            ((ViewModelBase) _activePage.BindingContext).RefreshTitle();
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

        public static int CurrentEnterpriseId
        {
            get => _currentEnterpriseId;
            set => _currentEnterpriseId = value;
        }

        public static ViewModelBase CurrentViewModel { get; set; }

        public static Action ActivePageChanged;
        public static Action CurrentUserChanged;

        private static UserInfo _currentUser;
        private static Page _activePage;
        private static int _currentEnterpriseId;
    }
}
