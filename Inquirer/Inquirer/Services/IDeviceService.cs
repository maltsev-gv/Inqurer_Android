using System.Collections.Generic;
using InquirerForAndroid.Models;
using Xamarin.Forms;

namespace InquirerForAndroid.Services
{
    public interface IDeviceService
    {
        string AppVersion { get; }
        string AppBuild { get; }
        string AppName { get; }
        string AppBundle { get; }
        string DeviceId { get; }
        string DeviceName { get; }
        string Os { get; }
        void CloseApp();
        void InstallApk(string fileName);
        string GetExternalStorage();
        void OpenAppInMarket(LocalPackageInfo packageInfo);
        void RunExternalApp(LocalPackageInfo packageInfo);
        List<LocalPackageInfo> GetInstalledApps();
        string GetAppNameFromPackage(string packageName);
        ImageSource GetImageSourceFromPackage(string packageName);
    }
}