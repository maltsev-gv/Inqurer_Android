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
            Globals.ActivePageChanged += () => { RaisePropertyChanged(nameof(HalfWidth)); };
        }
        public IDataStore DataStore => DependencyService.Get<IDataStore>();

        public bool IsRefreshing
        {
            get => GetVal<bool>();
            set => SetVal(value);
        }

        public virtual string Title
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
                    // LauncherPage.Alert("Ошибка", value, null, "Закрыть");
                }
            }
        }

        public double HalfWidth => DeviceDisplay.MainDisplayInfo.Width / 4;
    }
}
