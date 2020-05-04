using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using InquirerForAndroid.ViewModels;
using Xamarin.Forms;

namespace InquirerForAndroid.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EnterpriseSelectorView : IScrollableView
    {
        public EnterpriseSelectorView()
        {
            Debug.WriteLine(" ctor EnterpriseSelectorView");
            Globals.CurrentViewModel = ViewModel;
            InitializeComponent();
        }

        public EnterpriseSelectorViewModel ViewModel => BindingContext as EnterpriseSelectorViewModel;

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.EnterpriseSelectedCommand.Execute(e.Item);
        }

        private void EnterpriseSelectorView_OnSizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("EnterpriseSelectorView_OnSizeChanged");
            IsScrolled = true;
            if (Globals.CurrentEnterpriseId != 0)
            {
                var info = ViewModel.Enterprises.FirstOrDefault(ei => ei.EnterpriseId == Globals.CurrentEnterpriseId);
                if (info != null)
                {
                    listView.ScrollTo(info, ScrollToPosition.Center, false);
                }
            }
        }

        private void EnterpriseSelectorView_OnMeasureInvalidated(object sender, EventArgs e)
        {
            Debug.WriteLine("EnterpriseSelectorView_OnMeasureInvalidated");
        }

        public bool IsScrolled { get; set; }
    }
}