﻿using System;
using System.ComponentModel;
using Android.Content;
using InquirerForAndroid.ViewModels;
using Xamarin.Forms;
using Application = Android.App.Application;

//using Xamarin.Forms;

namespace InquirerForAndroid.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            Globals.CurrentViewModel = ViewModel;
            //Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);
        }

        public RegistrationViewModel ViewModel => BindingContext as RegistrationViewModel;

    }
}