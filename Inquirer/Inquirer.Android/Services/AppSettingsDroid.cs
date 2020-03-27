﻿using Android.Content;
using Android.Content.PM;
using InquirerForAndroid.Droid.Services;
using InquirerForAndroid.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppSettingsDroid))]
namespace InquirerForAndroid.Droid.Services
{
    public class AppSettingsDroid : IAppSettings
    {
        PackageInfo _appInfo;
        Context _context;
        public AppSettingsDroid()
        {
            _context = Android.App.Application.Context;
            _appInfo = _context.PackageManager.GetPackageInfo(_context.PackageName, 0);
        }

        public string AppVersion
        {
            get
            {
                return _appInfo.VersionName;
            }
        }
        public string AppBuild
        {
            get
            {
                return _appInfo.VersionCode.ToString();
            }
        }
        public string AppName
        {
            get
            {
                return _appInfo.ApplicationInfo.LoadLabel(_context.PackageManager);
            }
        }

        public string AppBundle
        {
            get
            {
                return _context.PackageName;
            }
        }

        public string AppPhone
        {
            get
            {
                return Android.OS.Build.Model;
            }
        }
    }
}