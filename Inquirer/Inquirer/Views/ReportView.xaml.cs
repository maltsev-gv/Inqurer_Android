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
    public partial class ReportView : IScrollableView
    {
        public ReportView()
        {
            Debug.WriteLine(" ctor ReportView");
            InitializeComponent();
            Globals.CurrentViewModel = this.BindingContext as ViewModelBase;
        }

        public bool IsScrolled { get; set; }

        private void ReportView_OnSizeChanged(object sender, EventArgs e)
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