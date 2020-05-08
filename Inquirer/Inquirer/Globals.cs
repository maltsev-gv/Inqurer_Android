using System;
using InquirerForAndroid.Models;
using InquirerForAndroid.ViewModels;
using Xamarin.Forms;

namespace InquirerForAndroid
{
    public class Globals
    {
        public static string BaseUrl = "http://84.201.162.94:45000/api/inquirer/";

        public static string DateFormat = "dd.MM.yyyy";

        public static UserInfo CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                CurrentUserChanged?.Invoke();
            }
        }

        public static int CurrentEnterpriseId { get; set; }

        public static ViewModelBase CurrentViewModel { get; set; }

        public static double ScreenWidth
        {
            get => _screenWidth;
            set
            {
                _screenWidth = value;
                ScreenSizeChanged?.Invoke();
            }
        }

        public static double ScreenHeight { get; set; }

        public static Action CurrentUserChanged;
        public static Action ScreenSizeChanged;

        private static UserInfo _currentUser;
        private static double _screenWidth;
    }
}
