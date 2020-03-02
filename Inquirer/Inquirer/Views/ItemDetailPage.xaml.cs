using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Inquirer.Models;
using Inquirer.ViewModels;

namespace Inquirer.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new ItemInfo
            {
                Text = "Item 1",
                Value = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}