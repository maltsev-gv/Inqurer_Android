using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using InquirerForAndroid.Models;
using InquirerForAndroid.ViewModels;
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
            Debug.WriteLine(" ctor EnterpriseSelectorPage");
            Globals.ActivePage = this;
            InitializeComponent();
        }

        public EnterpriseSelectorViewModel ViewModel => BindingContext as EnterpriseSelectorViewModel;

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.EnterpriseSelectedCommand.Execute(e.Item);
        }

        private void EnterpriseSelectorPage_OnAppearing(object sender, EventArgs e)
        {
            Debug.WriteLine("EnterpriseSelectorPage OnAppearing");
            ViewModel.Title = "Выбор предприятия";
            (BindingContext as ViewModelBase)?.RefreshTitle();
            if (Globals.CurrentEnterpriseId != 0)
            {
                var info = ViewModel.Enterprises.FirstOrDefault(ei => ei.EnterpriseId == Globals.CurrentEnterpriseId);
                if (info != null)
                {
                    listView.ScrollTo(info, ScrollToPosition.Center, false);
                }
            }
        }
    }
}