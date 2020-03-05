using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Inquirer_Android
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private void AppShell_OnSizeChanged(object sender, EventArgs e)
        {
            Globals.ActivePageChanged?.Invoke();
        }
    }
}
