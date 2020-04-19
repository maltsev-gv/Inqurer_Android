using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Input;
using InquirerForAndroid.Models;
using Rcn.Common;
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

        private async void LoadNewsMethod()
        {
            try
            {
                var news = await DataStore.GetNews(true);
                if (news == null)
                {
                    return;
                }
                NewsBlocks = new ObservableCollection<NewsBlockInfo>(news);
                NewsBlocksChanged?.Invoke();
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

        public ICommand LoadNewsCommand { get; set; }

        public Action NewsBlocksChanged;

        public ObservableCollection<NewsBlockInfo> NewsBlocks
        {
            get => GetVal<ObservableCollection<NewsBlockInfo>>();
            set => SetVal(value);
        }

    }
}
