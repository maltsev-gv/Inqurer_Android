using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Inquirer_Android
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

        public static Action ActivePageChanged;
    }
}
