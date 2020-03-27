using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using InquirerForAndroid.Models;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.ViewModels
{
    public class EnterpriseSelectorViewModel : ViewModelBase
    {
        public List<EnterpriseInfo> Enterprises
        {
            get => GetVal<List<EnterpriseInfo>>();
            set => SetVal(value);
        }

        public async Task LoadItemsMethod(bool forceRefresh = true)
        {
            if (IsRefreshing)
            {
                return;
            }

            IsRefreshing = true;

            try
            {
                Enterprises.Clear();
                Enterprises = await DataStore.GetEnterprises(forceRefresh);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                if (forceRefresh || Enterprises.Count == 0)
                {
                    await AppShell.Alert("Ошибка связи", ex.Message, null, "Закрыть");
                }
            }
            finally
            {
                IsRefreshing = false;
                //RaisePropertyChanged(nameof(IsNoDocumentsPresent));
            }
        }
    }
}
