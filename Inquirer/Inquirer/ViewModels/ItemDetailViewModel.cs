using System;
using InquirerForAndroid.Models;

namespace InquirerForAndroid.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ItemInfo Item { get; set; }
        public ItemDetailViewModel(ItemInfo item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
