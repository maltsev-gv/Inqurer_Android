using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using InquirerForAndroid.ViewModels;
using Rcn.Common.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InquirerForAndroid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WrapperPage : ContentPage
    {
        public WrapperPage() 
        {
            Debug.WriteLine("WrapperPage ctor ");
            BindingContext = _wrapperViewModel = new WrapperViewModel();
            _wrapperPage = this;
            InitializeComponent();
            _wrapperViewModel.ActiveView = (ContentView) grid.Children.Single(v => v is AuthView);
            _wrapperViewModel.ActiveView.IsVisible = true;
        }

        private static Regex _viewModelRegex = new Regex("(.+)ViewModel");
        private static WrapperViewModel _wrapperViewModel;
        private static WrapperPage _wrapperPage;

        private static async Task ScrollTo(IScrollableView destView, IScrollableView sourceView, bool forward)
        {
            if (!forward)
            {
                destView.HideContent();
                await _wrapperPage.scrollView.ScrollToAsync(sourceView as View, ScrollToPosition.Start, false);
                destView.ShowContent();
                //await _wrapperPage.scrollView.ScrollToAsync(-360, 0, true);
                //return;
            }

            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(0);

            DurationHelper.InitTiming("scroll");
            var animation = new Animation(
                callback: async x =>
                {
                    await _wrapperPage.scrollView.ScrollToAsync(x, 0, false);
                    Debug.WriteLine($"Animation: {x} ({DurationHelper.GetSecondsString("scroll")})");
                },
                start: _wrapperPage.scrollView.ScrollX,
                end: forward ? Globals.ScreenWidth : -Globals.ScreenWidth);

            animation.Commit(
                owner: _wrapperPage.scrollView,
                name: "Scroll",
                length: destView.IsScrolled ? (uint)300 : 700,
                easing: Easing.CubicIn, 
                finished: (finalValue, isCancelled) =>
                {
                    semaphoreSlim.Release();
                    Debug.WriteLine($"Animation finished: {finalValue}, {isCancelled} ({DurationHelper.GetSecondsString("scroll")})");
                });
            await semaphoreSlim.WaitAsync();
        }

        public static async void GoToView(ViewModelBase viewModel, bool isScrollNeeded = true, bool forward = true)
        {
            if (_wrapperPage == null)
            {
                _pendingViewModel = viewModel;
                return;
            }

            var view = GetViewByModel(viewModel, forward);
            var usePreviousViewModel = viewModel is AuthViewModel || viewModel is EnterpriseSelectorViewModel;
            if (usePreviousViewModel)
            {
                viewModel = view.BindingContext as ViewModelBase;
            }

            Globals.CurrentViewModel = viewModel;

            view.IsVisible = true;
            ContentView prevActiveView = null;
            if (isScrollNeeded)
            {
                prevActiveView = _wrapperViewModel.ActiveView;
                await ScrollTo(view as IScrollableView, prevActiveView as IScrollableView, forward);
            }

            _wrapperViewModel.ActiveView = view;
            _wrapperViewModel.ActiveView.BindingContext = viewModel;
            if (prevActiveView != null)
            {
                prevActiveView.IsVisible = false;
            }
        }

        private static ContentView GetViewByModel(ViewModelBase viewModel, bool forward)
        {
            var typeName = viewModel.GetType().Name;
            var match = _viewModelRegex.Match(typeName);
            if (!match.Success)
            {
                throw new ArgumentException($"{nameof(GetViewByModel)}: invalid view model: {viewModel}");
            }
            var viewName = $"{match.Groups[1].Value}View";
            return (ContentView)_wrapperPage.grid.Children.Single(v => v.GetType().Name == viewName);
        }

        private static ViewModelBase _pendingViewModel;
        private void WrapperPage_OnAppearing(object sender, EventArgs e)
        {
            if (_pendingViewModel != null)
            {
                GoToView(_pendingViewModel, false);
                _pendingViewModel = null;
            }
        }
    }
}