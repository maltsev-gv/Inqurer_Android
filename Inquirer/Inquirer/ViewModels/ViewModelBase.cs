using System;
using InquirerForAndroid.Services;
using Rcn.Common;
using Rcn.Common.ExtensionMethods;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace InquirerForAndroid.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public ViewModelBase()
        {
            Globals.ActivePageChanged += () =>
            {
                RaisePropertyChanged(nameof(HalfWidth));
                RaisePropertyChanged(nameof(QuarterWidth));
            };
        }
        public IDataStore DataStore => DependencyService.Get<IDataStore>();
        public IDeviceService DeviceService => DependencyService.Get<IDeviceService>();

        public bool IsRefreshing
        {
            get => GetVal<bool>();
            set => SetVal(value);
        }

        public string Title
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public string ErrorMessage
        {
            get => GetVal<string>();
            set
            {
                value = value.Trim('"');
                SetVal(value);
                if (!value.IsNullOrEmpty())
                {
                    AppShell.Alert("Ошибка", value, null, "Закрыть");
                }
            }
        }

        public double HalfWidth => DeviceDisplay.MainDisplayInfo.Width / 4;
        public double QuarterWidth => DeviceDisplay.MainDisplayInfo.Width / 8;
    }
}
