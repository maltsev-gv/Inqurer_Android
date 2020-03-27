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
        private static Page _activePage;
        public static Page ActivePage
        {
            get => _activePage;
            set
            {
                _activePage = value;
                ActivePageChanged?.Invoke();
            }
        }

        public static UserInfo CurrentUser { get; set; }
        public static ViewModelBase CurrentViewModel { get; set; }

        public static Action ActivePageChanged;
    }
}
