using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using InquirerForAndroid.Models;
using InquirerForAndroid.Services;
using InquirerForAndroid.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
            NewsViewModel.NewsBlocksChanged = OnNewsBlocksChanged;
        }

        private void OnScrollMethod(Point scrollPoint)
        {
            Debug.WriteLine($"OnScrollMethod: {scrollPoint.X}, {scrollPoint.Y}");
        }

        public NewsPage(bool isShown) : this()
        {
            //BindingContext = NewsViewModel = vm;
            //vm.NewsBlocksChanged = OnNewsBlocksChanged;
            if (isShown)
            {
                labelWelcome.IsVisible = false;
            }
        }

        private void OnNewsBlocksChanged()
        {
            _labels.Clear();
            if (_itemIdToScroll != -1 && !_isAutoScrolled)
            {
                var item = ((NewsViewModel)BindingContext).NewsBlocks?.FirstOrDefault(nb => nb.NewsBlockId == _itemIdToScroll);
                if (item != null)
                {
                    //listView.ScrollTo(item, ScrollToPosition.Start, false);
                    DependencyService.Get<IVisualService>().ScrollListViewTo(listView, 0, _listScrollY);
                    //listView.SendScrolled(new ScrolledEventArgs(0, _listScrollY));
                    Debug.WriteLine($"NewsPage_OnLayoutChanged: scroll to {_listScrollY}, item = {item.Title}");
                    _isAutoScrolled = true;
                }
            }
        }

        private int _maxNewsBlockHeight = 200;
        public static NewsViewModel NewsViewModel;
        private Dictionary<NewsBlockInfo, Label> _labels = new Dictionary<NewsBlockInfo, Label>();

        private void ItemsListView_OnScrollToRequested(object sender, ScrollToRequestedEventArgs e)
        {
            var elem = e.Element;
        }

        private void LabelText_OnBindingContextChanged(object sender, EventArgs e)
        {
            var label = (Label)sender;
            var info = (NewsBlockInfo)label.BindingContext;
            if (_labels.ContainsKey(info))
            {
                return;
            }

            var height = DependencyService.Get<IVisualService>()
                .MeasureTextSize(label.Text, listView.Width, label.FontSize, label.FontFamily);
            if (height > _maxNewsBlockHeight)
            {
                info.CanBeExpanded = true;
                _labels[info] = label;
                if (!_expandedItemIds.ContainsKey(info.NewsBlockId) || !_expandedItemIds[info.NewsBlockId])
                {
                    label.HeightRequest = _maxNewsBlockHeight;
                }
            }
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var info = (NewsBlockInfo) e.Item;
            if (info.CanBeExpanded)
            {
                info.IsExpanded = !info.IsExpanded;

                var label = _labels[info];
                var height = DependencyService.Get<IVisualService>()
                    .MeasureTextSize(label.Text, listView.Width, label.FontSize, label.FontFamily);

                label.HeightRequest = info.IsExpanded ? height : _maxNewsBlockHeight;
                var viewCell = (ViewCell) label.Parent.Parent;
                viewCell.ForceUpdateSize();
                _expandedItemIds[info.NewsBlockId] = info.IsExpanded;
                Debug.WriteLine($"ListView_OnItemTapped: info {info.GetHashCode()}, viewCell {viewCell.GetHashCode()}");

            }
        }

        private static int _listScrollY = 0;
        private static int _itemIdToScroll;
        private static Dictionary<int, bool> _expandedItemIds = new Dictionary<int, bool>();
        private bool _isAutoScrolled;
        private void ListView_OnScrolled(object sender, ScrolledEventArgs e)
        {
            _listScrollY = (int) e.ScrollY;
            var items = DependencyService.Get<IVisualService>().GetListViewVisibleItems<NewsBlockInfo>(listView);
            _itemIdToScroll = items.Count > 1 ? items[1].NewsBlockId
                : items.Count == 1 ? items[0].NewsBlockId
                : -1;
            Debug.WriteLine($"ListView_OnScrolled: Y = {_listScrollY}, item {_itemIdToScroll}");
        }

        private void ListView_OnScrollToRequested(object sender, ScrollToRequestedEventArgs e)
        {
        }

        //private Dictionary<NewsBlockInfo, int> _visibleIndexes = new Dictionary<NewsBlockInfo, int>();
        //private void ListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        //{
        //    var item = (NewsBlockInfo)e.Item;
        //    _visibleIndexes[item] = e.ItemIndex;
        //    Debug.WriteLine($"ItemAppearing : {item.Title} ({e.ItemIndex})");
        //}

        //private void ListView_OnItemDisappearing(object sender, ItemVisibilityEventArgs e)
        //{
        //    var item = (NewsBlockInfo)e.Item;
        //    _visibleIndexes[item] = -1;
        //    Debug.WriteLine($"ItemDisappearing : {item.Title} ({e.ItemIndex})");
        //}
        private void ListView_OnBindingContextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("ListView_OnBindingContextChanged");
        }

        private void ListView_OnMeasureInvalidated(object sender, EventArgs e)
        {
            Debug.WriteLine($"ListView_OnMeasureInvalidated");
        }

        private void ListView_OnSizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("ListView_OnSizeChanged");
        }

        private void ListView_OnBatchCommitted(object sender, EventArg<VisualElement> e)
        {
            Debug.WriteLine("ListView_OnBatchCommitted");
        }

        private bool _isAppearing;
        private void NewsPage_OnAppearing(object sender, EventArgs e)
        {
            Debug.WriteLine("NewsPage_OnAppearing");
            _isAppearing = true;
        }

        private void NewsPage_OnLayoutChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("NewsPage_OnLayoutChanged");
            if (_isAppearing)
            {
                _isAppearing = false;
                //DependencyService.Get<IVisualService>().SignToListViewScrollEvent(listView, OnScrollMethod);
            }
        }

        private void NewsPage_OnMeasureInvalidated(object sender, EventArgs e)
        {
            Debug.WriteLine("NewsPage_OnMeasureInvalidated");
        }

        private void NewsPage_OnDisappearing(object sender, EventArgs e)
        {
            Debug.WriteLine($"NewsPage_OnDisappearing: {_listScrollY}");
        }

        //public static NewsViewModel SavedNewsViewModel;
        private void NewsPage_OnBindingContextChanged(object sender, EventArgs e)
        {
            //if (BindingContext is NewsViewModel newsViewModel)
            //{
            //    SavedNewsViewModel = newsViewModel;
            //}
        }
    }
}