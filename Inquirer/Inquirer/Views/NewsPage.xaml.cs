using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using InquirerForAndroid.Models;
using InquirerForAndroid.Services;
using InquirerForAndroid.ViewModels;
using Xamarin.Forms;

namespace InquirerForAndroid.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();
            Globals.ActivePage = this;
            NewsViewModel = (NewsViewModel) BindingContext;
        }

        public static NewsViewModel NewsViewModel;

        private int _maxNewsBlockHeight = 200;
        private static int _listScrollY;
        private static int _itemIdToScroll;

        private void LabelText_OnBindingContextChanged(object sender, EventArgs e)
        {
            var label = (Label)sender;
            var info = (NewsBlockInfo)label.BindingContext;

            var height = DependencyService.Get<IVisualService>()
                .MeasureTextSize(label.Text, listView.Width, label.FontSize, label.FontFamily);
            info.CanBeExpanded = height > _maxNewsBlockHeight;
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var info = (NewsBlockInfo) e.Item;
            if (info.CanBeExpanded)
            {
                info.IsExpanded = !info.IsExpanded;
            }
        }

        private void ListView_OnScrolled(object sender, ScrolledEventArgs e)
        {
            if (_isAppearing)
            {
                return;
            }
            _listScrollY = (int) e.ScrollY;
            var items = DependencyService.Get<IVisualService>().GetListViewVisibleItems<NewsBlockInfo>(listView);
            _itemIdToScroll = items.Count > 1 ? items[1].NewsBlockId
                : items.Count == 1 ? items[0].NewsBlockId
                : -1;
            Debug.WriteLine($"ListView_OnScrolled: Y = {_listScrollY}, item {_itemIdToScroll}");
        }

        private bool _isAppearing;
        private void NewsPage_OnAppearing(object sender, EventArgs e)
        {
            Debug.WriteLine("NewsPage_OnAppearing");
            _isAppearing = true;
        }

        private void NewsPage_OnMeasureInvalidated(object sender, EventArgs e)
        {
            Debug.WriteLine("NewsPage_OnMeasureInvalidated");
            if (_isAppearing)
            {
                var item = NewsViewModel?.NewsBlocks?.FirstOrDefault(nb => nb.NewsBlockId == _itemIdToScroll);
                if (item != null)
                {
                    listView.ScrollTo(item, ScrollToPosition.MakeVisible, false);
                    DependencyService.Get<IVisualService>().ScrollListViewTo(listView, 0, _listScrollY);
                }
                Device.BeginInvokeOnMainThread(() => _isAppearing = false);
            }
        }
    }
}