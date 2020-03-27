using System.Collections.ObjectModel;
using InquirerForAndroid.Models;

namespace InquirerForAndroid.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        public AppShellViewModel()
        {
            MenuItems = new ObservableCollection<MenuItemInfo>()
            {
                new MenuItemInfo() { Text = "menu item 1"},
                new MenuItemInfo() { Text = "menu item 2"},
                new MenuItemInfo() { Text = "menu item 3"},
            };
        }
        public ObservableCollection<MenuItemInfo> MenuItems { get; set; }
    }
}
