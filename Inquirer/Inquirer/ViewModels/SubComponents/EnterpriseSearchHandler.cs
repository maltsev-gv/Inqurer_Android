using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InquirerForAndroid.Models;
using InquirerForAndroid.Services;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels.SubComponents
{
    public class EnterpriseSearchHandler : SearchHandler
    {
        protected async override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                var dataStore = DependencyService.Get<IDataStore>();

                ItemsSource = (await dataStore.GetEnterprises(false))
                    .Where(enterpriseInfo => enterpriseInfo.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            var enterpriseInfo = item as EnterpriseInfo;
            if (enterpriseInfo is null) return;
        }
    }
}
