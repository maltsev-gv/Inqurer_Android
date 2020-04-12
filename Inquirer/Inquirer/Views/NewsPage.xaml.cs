using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            _newsViewModel = (NewsViewModel) BindingContext;
            _newsViewModel.NewsBlocksChanged = OnNewsBlocksChanged;
        }

        private void OnNewsBlocksChanged()
        {
            _labels.Clear();
        }

        private int _maxNewsBlockHeight = 200;
        private NewsViewModel _newsViewModel;
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
                label.HeightRequest = _maxNewsBlockHeight;
                _labels[info] = label;
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
            }
        }
    }
}