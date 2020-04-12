using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Inquirer.Droid.Services;
using InquirerForAndroid.Models;
using InquirerForAndroid.Services;
using Xamarin.Forms;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(DeviceServiceDroid))]
namespace Inquirer.Droid.Services
{
    public class DeviceServiceDroid : IDeviceService
    {
        public DeviceServiceDroid()
        {
            _context = Android.App.Application.Context;
            _appInfo = _context.PackageManager.GetPackageInfo(_context.PackageName, 0);
        }

        public string AppVersion => _appInfo.VersionName;
    
        public string AppBuild => _appInfo.VersionCode.ToString();

        private string _appName;
        public string AppName => _appName ??= _appInfo.ApplicationInfo.LoadLabel(_context.PackageManager);

        public string AppBundle => _context.PackageName;

        private string _deviceId;
        public string DeviceId => _deviceId ??= Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver,
            Android.Provider.Settings.Secure.AndroidId);

        public string DeviceName => $"{Build.Model} {Build.Device}";
 
        public string Os => nameof(Android);
        
        public void CloseApp()
        {
            Process.KillProcess(Process.MyPid());
        }

        public void OpenAppInMarket(LocalPackageInfo packageInfo)
        {
            try
            {
                Intent intent = new Intent(Intent.ActionView, Uri.Parse("market://details?id=" + packageInfo.PackageName));
                intent.AddFlags(ActivityFlags.NewTask);
                _context.StartActivity(intent);
            }
            catch (ActivityNotFoundException)
            {
                var intent = new Intent(Intent.ActionView,
                    Uri.Parse("http://play.google.com/store/apps/details?id=" + packageInfo.PackageName));
                intent.AddFlags(ActivityFlags.NewTask);
                _context.StartActivity(intent);
            }
        }

        public void RunExternalApp(LocalPackageInfo packageInfo)
        {
            Intent intent = _context.PackageManager.GetLaunchIntentForPackage(packageInfo.PackageName);
            _context.StartActivity(intent);
        }

        public void InstallApk(string fileName)
        {
            File file = new File(fileName);
            Intent install;
            Uri apkUri;
            if (Build.VERSION.SdkInt > BuildVersionCodes.N)
            {
                apkUri = FileProvider.GetUriForFile(_context, _context.ApplicationContext.PackageName + ".com.package.name.provider", file);
                install = new Intent(Intent.ActionInstallPackage);
                install.SetFlags(ActivityFlags.GrantReadUriPermission | ActivityFlags.NewTask);
            }
            else
            {
                apkUri = Uri.FromFile(file);
                install = new Intent(Intent.ActionView);
                install.SetFlags(ActivityFlags.NewTask);
            }
            install.SetDataAndType(apkUri, "application/vnd.android.package-archive");
            _context.StartActivity(install);
        }

        public List<LocalPackageInfo> GetInstalledApps()
        {
            var installedApps = _context.PackageManager.GetInstalledApplications(PackageInfoFlags.MatchAll);
            var appList = installedApps.Select(applicationInfo => new LocalPackageInfo(applicationInfo.PackageName)).ToList();
            return appList;
        }

        public string GetAppNameFromPackage(string packageName)
        {
            var appInfo = _context.PackageManager.GetApplicationInfo(packageName, PackageInfoFlags.MatchAll);
            return appInfo.LoadLabel(_context.PackageManager);
        }

        public ImageSource GetImageSourceFromPackage(string packageName)
        {
            var appInfo = _context.PackageManager.GetApplicationInfo(packageName, PackageInfoFlags.MatchAll);
            var icon = appInfo.LoadIcon(_context.PackageManager);
            var bitmap = Bitmap.CreateBitmap(icon.IntrinsicWidth, icon.IntrinsicHeight, Bitmap.Config.Argb8888);
            using (var canvas = new Canvas(bitmap))
            {
                icon.SetBounds(0, 0, canvas.Width, canvas.Height);
                icon.Draw(canvas);
            }

            return ImageSource.FromStream(() =>
            {
                MemoryStream ms = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
                ms.Seek(0L, SeekOrigin.Begin);
                return ms;
            });
        }

        public string GetExternalStorage()
        {
            Context context = Android.App.Application.Context;
            var filePath = context.GetExternalFilesDir("");
            return filePath.Path;
        }

        private readonly PackageInfo _appInfo;
        private readonly Context _context;
    }
}