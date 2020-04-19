using System.ComponentModel;
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
            //Shell.Current.FlyoutIsPresented = true;

            Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                IconOverride = "About.png",
                IsEnabled = false,
                Command = new Command(() =>
                {
                    for (var index = 0; index < Shell.Current.Navigation.NavigationStack.Count; index++)
                    {
                        Shell.Current.Navigation.PopAsync();
                    }

                })
            });
            //Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);
        }

    }
}