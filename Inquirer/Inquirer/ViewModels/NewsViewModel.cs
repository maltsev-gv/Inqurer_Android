using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using InquirerForAndroid.Models;
using Rcn.Common.ExtensionMethods;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        public NewsViewModel()
        {
            Title = "Новости РУК";
            LoadNewsCommand = new Command(LoadNewsMethod);
            LoadNewsCommand.Execute(null);
        }

        private Dictionary<int, bool> _expandedNews = new Dictionary<int, bool>();

        private async void LoadNewsMethod()
        {
            try
            {
                if (NewsBlocks?.Count > 0)
                {
                    _expandedNews = NewsBlocks.ToDictionary(nb => nb.NewsBlockId, nb => nb.IsExpanded);
                }

                var news = await DataStore.GetNews(true);
                if (news == null)
                {
                    return;
                }
                NewsBlocks = new ObservableCollection<NewsBlockInfo>(news);
                NewsBlocks.ForEach(nb => nb.CanBeExpandedCalculated = CanBeExpandedCalculated);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private void CanBeExpandedCalculated(NewsBlockInfo info)
        {
            if (!info.CanBeExpanded)
            {
                info.IsExpanded = true; // у малых блоков ставим автовысоту 
                return;
            }

            // по умолчанию сворачиваем новые крупные блоки новостей. Если блок был ранее свернут/развернут - восстанавливаем его состояние
            info.IsExpanded = _expandedNews.ContainsKey(info.NewsBlockId) && _expandedNews[info.NewsBlockId];
        }

        public ICommand LoadNewsCommand { get; set; }

        public ObservableCollection<NewsBlockInfo> NewsBlocks
        {
            get => GetVal<ObservableCollection<NewsBlockInfo>>();
            set => SetVal(value);
        }

    }
}
