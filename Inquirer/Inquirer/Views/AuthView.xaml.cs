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
    public partial class AuthView : IScrollableView
    {
        public AuthView()
        {
            Debug.WriteLine(" ctor AuthView");
            InitializeComponent();
            Globals.CurrentViewModel = this.BindingContext as ViewModelBase;
        }

        public bool IsScrolled { get; set; }

        private void AuthView_OnSizeChanged(object sender, EventArgs e)
        {
            IsScrolled = true;
        }

        public void HideContent()
        {
            mainGrid.IsVisible = false;
        }

        public void ShowContent()
        {
            mainGrid.IsVisible = true;
        }
    }
}