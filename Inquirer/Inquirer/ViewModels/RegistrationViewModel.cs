using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using Rcn.Common;
using Rcn.Common.ExtensionMethods;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public RegistrationViewModel()
        {
            Title = "Добро пожаловать";
            ButtonStateText = "Скачать приложение";
            RegisterCommand = new Command(RegisterMethod);
            DownloadCommand = new Command(DownloadMethod);
            SetupCommand = new Command(SetupMethod);
            var path = DeviceService.GetExternalStorage();
            var files = Directory.EnumerateFiles(path, "*.apk").ToArray();
            if (files.Length > 0)
            {
                ApkName = new FileInfo(files[0]).Name;
                FileName = files[0];
            }
        }

        private async void DownloadMethod()
        {
            try
            {
                IsRefreshing = true;
                ButtonStateText = _downloadText;
                var fileName = await DataStore.GetApk(1, OnProgressChanged);
                FileName = fileName;
                ApkName = new FileInfo(fileName).Name;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private void SetupMethod()
        {
            var installed = DeviceService.GetInstalledApps();
            DeviceService.InstallApk(FileName);
        }

        private const string _downloadText = "Скачивание: ";

        //private Timer _timer = new Timer(200) { AutoReset = true };

        private DateTime startTime;

        private async void RegisterMethod()
        {
            try
            {
                var user = await DataStore.Auth(Login);
                if (user == null)
                {
                    await AppShell.Alert("Вход не удался",
                        "Введен неверный PIN-код, либо данный PIN-код уже не действителен.",
                        null, "ОК");
                    return;
                }
                Globals.CurrentUser = user;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsRefreshing = false;
            }

            AppShell.GoToPage(new EnterpriseSelectorViewModel());


            //DeviceService.RunExternalApp();

            //if (Password.IsNullOrWhiteSpace())
            //{
            //    var registerAnyway = await AppShell.Alert("Не указан логин/пароль",
            //        "Вы ввели только PIN-код. Уверены, что хотите продолжить?",
            //        "Да", "Нет");
            //    if (!registerAnyway)
            //    {
            //        return;
            //    }
            //}

        }

        private void OnProgressChanged(double part)
        {
            ButtonStateText = $"Скачано {part * 100:F1}%";
        }

        public string ButtonStateText
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public string ApkName
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public string Login
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public string FileName
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public string Pin
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public ICommand RegisterCommand { get; set; }
        public ICommand DownloadCommand { get; set; }
        public ICommand SetupCommand { get; set; }

        //public object BackCommand
        //{
        //    get { throw new System.NotImplementedException(); }
        //}
    }
}
