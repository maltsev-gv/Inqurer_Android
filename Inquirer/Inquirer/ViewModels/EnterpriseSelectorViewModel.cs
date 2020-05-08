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
    public class EnterpriseSelectorViewModel : ViewModelBase
    {
        public EnterpriseSelectorViewModel()
        {
            //GoToAuth();
            Debug.WriteLine($"EnterpriseSelectorViewModel: ctor");
            Title = "Выбор предприятия";
            LoadEnterprisesCommand = new Command(async isForced => await LoadItemsMethod(isForced as bool? ?? true));
            ExpandItemCommand = new Command(ExpandItemMethod);
            EnterpriseSelectedCommand = new Command(EnterpriseSelectedMethod);
            LoadEnterprisesCommand.Execute(false);
        }

        private void EnterpriseSelectedMethod(object obj)
        {
            var info = (EnterpriseInfo) obj;
            Globals.CurrentEnterpriseId = info.EnterpriseId;
            WrapperPage.GoToView(viewModel: new SurveySelectorViewModel(info));
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
        public ICommand EnterpriseSelectedCommand { get; set; }

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

        public override bool IsSameAs(ViewModelBase viewModel)
        {
            return viewModel is EnterpriseSelectorViewModel;
        }
    }
}
