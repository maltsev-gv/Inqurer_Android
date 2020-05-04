using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InquirerForAndroid.Models;
using InquirerForAndroid.Services;
using InquirerForAndroid.Views;
using Rcn.Common.ExtensionMethods;
using Rcn.Interfaces.Inquirer;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class SurveySelectorViewModel : ViewModelBase
    {
        public SurveySelectorViewModel()
        {
            //GoToAuth();
            Debug.WriteLine($"SurveySelectorViewModel: ctor");
            Title = "Выбор опроса";
            IsBackButtonPresent = true;
            LoadEnterprisesCommand = new Command(async isForced => await LoadItemsMethod(isForced as bool? ?? true));
            ExpandItemCommand = new Command(ExpandItemMethod);
            LoadEnterprisesCommand.Execute(false);
        }

        private void ExpandItemMethod(object obj)
        {
            var info = (EnterpriseInfo)obj;
            var isChildrenVisible = !info.IsExpanded;
            info.Children.ForEach(ei => ((EnterpriseInfo)ei).IsVisible = isChildrenVisible);
            info.RaiseAll();
            SaveEnterprisesVisibility();
        }

        public ICommand LoadEnterprisesCommand { get; set; }
        public ICommand ExpandItemCommand { get; set; }
        public ICommand SearchEnterpriseCommand { get; set; }

        private void GoToAuth()
        {
            AppShell.GoToPage(new AuthViewModel());
        }

        protected override void OnBackButtonPressed()
        {
            WrapperPage.GoToView(new EnterpriseSelectorViewModel(), forward: false);
        }

        public List<EnterpriseInfo> Enterprises
        {
            get => GetVal<List<EnterpriseInfo>>();
            set => SetVal(value);
        }

        public string FilterText
        {
            get => GetVal<string>();
            set
            {
                SetVal(value);
                if (!value.IsNullOrEmpty())
                {
                    var lowerText = value.ToLower();
                    Enterprises.ForEach(ei => ei.IsVisible = ei.Name.ToLower().Contains(lowerText) || ei.ShortName.ToLower().Contains(lowerText));
                }
                else
                {
                    RestoreEnterprisesVisibility();
                }
            }
        }

        private List<EnterpriseInfo> _rawEnterprises;
        private static Dictionary<int, bool> _visibleEnterprises = new Dictionary<int, bool>();

        public SurveySelectorViewModel(EnterpriseInfo info) : this()
        {
        }

        public async Task LoadItemsMethod(bool forceRefresh)
        {
            if (IsRefreshing)
            {
                return;
            }

            IsRefreshing = true;

            try
            {
                SaveEnterprisesVisibility();
                _rawEnterprises = await DataStore.GetEnterprises(forceRefresh);
                Enterprises = _rawEnterprises.GetFlatEnterprisesList();
                RestoreEnterprisesVisibility();
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

        private void SaveEnterprisesVisibility()
        {
            _visibleEnterprises = Enterprises?.ToDictionary(e => e.EnterpriseId, e => e.IsVisible)
                                  ?? new Dictionary<int, bool>();
        }

        private void RestoreEnterprisesVisibility()
        {
            Enterprises.ForEach(e =>
            {
                if (_visibleEnterprises.ContainsKey(e.EnterpriseId))
                {
                    e.IsVisible = _visibleEnterprises[e.EnterpriseId];
                }
            });
            Enterprises.Where(ei => ei.Parent == null).ForEach(ei => ei.IsVisible = true);
        }
    }
}
