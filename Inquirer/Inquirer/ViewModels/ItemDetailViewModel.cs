using System;
using Inquirer_Android.Models;

namespace Inquirer_Android.ViewModels
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
