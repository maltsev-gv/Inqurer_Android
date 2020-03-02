using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Inquirer.Models;

namespace Inquirer.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public ItemInfo Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new ItemInfo
            {
                Text = "Item name",
                Value = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}