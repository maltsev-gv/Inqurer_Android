using System;
using System.Windows.Input;
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
            Globals.ScreenSizeChanged += () =>
            {
                RaisePropertyChanged(nameof(FullWidth));
                RaisePropertyChanged(nameof(HalfWidth));
                RaisePropertyChanged(nameof(QuarterWidth));
            };
            BackButtonPressedCommand = new Command(OnBackButtonPressed);
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

        public bool IsBackButtonPresent
        {
            get => GetVal<bool>();
            set => SetVal(value);
        }

        public ICommand BackButtonPressedCommand { get; }

        protected virtual void OnBackButtonPressed()
        {
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

        public double FullWidth => Globals.ScreenWidth;
        public double HalfWidth => FullWidth / 2;
        public double QuarterWidth => FullWidth / 4;
    }
}
