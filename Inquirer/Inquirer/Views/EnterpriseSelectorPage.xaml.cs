﻿using System.ComponentModel;
using Xamarin.Forms;

namespace InquirerForAndroid.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EnterpriseSelectorPage : ContentPage
    {
        public EnterpriseSelectorPage()
        {
            InitializeComponent();
            Globals.ActivePage = this;
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);
        }

    }
}