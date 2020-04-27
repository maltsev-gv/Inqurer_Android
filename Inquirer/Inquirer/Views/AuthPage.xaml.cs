using System;
using System.ComponentModel;
using System.Diagnostics;
using InquirerForAndroid.ViewModels;
using Xamarin.Forms;

namespace InquirerForAndroid.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            Debug.WriteLine(" ctor AuthPage");
            Globals.ActivePage = this;
            InitializeComponent();
        }
    }
}