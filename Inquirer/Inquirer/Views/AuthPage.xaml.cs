﻿using System.ComponentModel;
using Xamarin.Forms;

namespace Inquirer_Android.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();
            Globals.ActivePage = this;
        }

    }
}