using System;

using Inquirer.Models;

namespace Inquirer.ViewModels
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
