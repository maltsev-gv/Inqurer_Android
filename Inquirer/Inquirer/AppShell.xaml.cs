using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Provider;
using InquirerForAndroid.Services;
using InquirerForAndroid.ViewModels;
using InquirerForAndroid.Views;
using Xamarin.Forms;
using Rcn.Common.ExtensionMethods;

namespace InquirerForAndroid
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(WrapperPage), typeof(WrapperPage));
            Routing.RegisterRoute(nameof(NewsPage), typeof(NewsPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));

            _surveyTab = surveyTab;
        }

        private static Tab _surveyTab;

        private static Type[] _viewsAtWrapper =
        {
            typeof(AuthView),
            typeof(EnterpriseSelectorView),
            typeof(SurveySelectorView),
        };

        private static Regex _viewModelRegex = new Regex("(.+)ViewModel");
        public static async void GoToPage(ViewModelBase viewModel)
        {
            var typeName = viewModel.GetType().Name;
            var match = _viewModelRegex.Match(typeName);
            if (!match.Success)
            {
                throw new ArgumentException($"{nameof(GoToPage)}: invalid view model: {viewModel}");
            }

            Globals.CurrentViewModel = viewModel;

            var viewName = $"{match.Groups[1].Value}View";
            var wrapperPage = _surveyTab.Items[0].Content as WrapperPage;
            var wrapperViewModel = (WrapperViewModel)wrapperPage.BindingContext;
            wrapperViewModel.ActiveView =
                (ContentView) Activator.CreateInstance(_viewsAtWrapper.Single(v => v.Name == viewName));
            wrapperViewModel.ActiveView.BindingContext = viewModel;
            //if (viewModel is SurveySelectorViewModel)
            //{
            //}
            //else
            //{
            //    await Current.GoToAsync($"{match.Groups[1].Value}Page");
            //}
        }

        private static string _lastMessage = "";
        public static async Task<bool> Alert(string title, string message, string accept, string cancel)
        {
            if (message == _lastMessage)
            {
                return false;
            }

            _lastMessage = message;
            var semaphore = new SemaphoreSlim(0, 3);
            bool result = false;

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (accept.IsNullOrEmpty())
                {
                    await Current.DisplayAlert(title, message, cancel);
                }
                else
                {
                    result = await Current.DisplayAlert(title, message, accept, cancel);
                }

                semaphore.Release();
            });
            await semaphore.WaitAsync();
            _lastMessage = "";
            return result;
        }

        // factory method to create MenuItem objects
        private static MenuItem CreateMenuItem(string title, ICommand cmd)
        {
            var menuItem = new MenuItem();
            menuItem.Text = title;
            menuItem.Command = cmd;
            return menuItem;
        }

        private void OnActivePageChanged()
        {
            //if (Globals.ActiveViewModel is EnterpriseSelectorView)
            //{
            //    Current.Items.Add(CreateMenuItem("Выбор предприятия", new Command(async () =>
            //    {
            //        await Current.Navigation.PushAsync(new EnterpriseSelectorView());
            //        Current.FlyoutIsPresented = false;
            //    })));
            //}
        }

        //public AppShell()
        //{
        //    // you can place this code in any method in the AppShell class
        //    Current.Items.Add(CreateMenuItem("AppInfo", new Command(async () =>
        //    {
        //        ShellNavigationState state = Shell.Current.CurrentState;
        //        await Shell.Current.Navigation.PushAsync(new AppInfoPage());
        //        Shell.Current.FlyoutIsPresented = false;
        //    })));
        //    ...
        //}
        public async void RaiseOnBackPressed()
        {
            if (Current.Navigation.ModalStack.Count > 0)
            {
                await Current.Navigation.PopModalAsync();
                return;
            }
            if (Current.Navigation.NavigationStack.Count > 1)
            {
                await Current.Navigation.PopAsync();
                return;
            }
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            var route = Routing.GetRoute((BindableObject)sender);
            await Current.GoToAsync(route);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var menuItem = ((Element)sender).GetParentOfType<MenuItem>();
            var route = Routing.GetRoute(menuItem);
            await Current.GoToAsync(route);
        }

        private void SurveyTab_OnAppearing(object sender, EventArgs e)
        {
            //var tab = (Tab)sender;

            //var page = tab.Items[0].Content;
            //if (Globals.CurrentUser == null)
            //{
            //    tab.Items[0] = new ShellContent()
            //    {
            //        Content = new AuthView()
            //    };
            //    return;
            //}
            //if (Globals.CurrentEnterpriseId != 0)
            //{
            //    tab.Items[0] = new ShellContent()
            //    {
            //        Content = new EnterpriseSelectorView()
            //    };
            //}
        }

        private void NewsTab_OnAppearing(object sender, EventArgs e)
        {
            //var tab = (Tab)sender;
            //tab.Items[0] = new ShellContent()
            //{
            //    Content = new NewsPage(NewsPage.NewsViewModel != null)
            //};
            //var vm = NewsPage.NewsViewModel;
            //if (vm != null)
            //{
            //    tab.Items[0] = new ShellContent()
            //    {
            //        Content = new NewsPage()
            //        {
            //            BindingContext = vm
            //        }
            //    };
            //}
        }

        private void AppShell_OnSizeChanged(object sender, EventArgs e)
        {
            Globals.ScreenHeight = Application.Current.MainPage.Height;
            Globals.ScreenWidth = Application.Current.MainPage.Width - 12;
            Debug.WriteLine($"Globals.ScreenWidth = {Globals.ScreenWidth}");
        }
    }
}
