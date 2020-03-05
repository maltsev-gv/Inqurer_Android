using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Inquirer_Android.Interfaces;
using Inquirer_Android.Models;
using Inquirer_Android.Views;

namespace Inquirer_Android.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<ItemInfo> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Опрос";
            Items = new ObservableCollection<ItemInfo>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, ItemInfo>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as ItemInfo;
                Items.Add(newItem);
                //await DataStore.AddItemAsync(newItem);
            });

            var settings = DependencyService.Get<IAppSettings>();
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                //foreach (var item in items)
                //{
                //    Items.Add(item);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}