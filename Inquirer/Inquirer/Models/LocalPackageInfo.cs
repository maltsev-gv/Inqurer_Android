using InquirerForAndroid.Services;
using Xamarin.Forms;

namespace InquirerForAndroid.Models
{
    public class LocalPackageInfo
    {
        public LocalPackageInfo(string packageName)
        {
            PackageName = packageName;
            _deviceService = DependencyService.Get<IDeviceService>();
        }

        public string AppName => _label ?? (_label = _deviceService.GetAppNameFromPackage(PackageName));
        public string PackageName { get; }

        public ImageSource Icon => _icon ?? (_icon = _deviceService.GetImageSourceFromPackage(PackageName));

        private ImageSource _icon;
        private string _label;
        private readonly IDeviceService _deviceService;
    }
}
