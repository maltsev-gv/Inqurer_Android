using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InquirerForAndroid.Models;
using InquirerForAndroid.Services;
using Rcn.Common.Exceptions;
using Rcn.Common.ExtensionMethods;
using Rcn.Common.Helpers;
using Rcn.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataStore))]
namespace InquirerForAndroid.Services
{
    public class DataStore : IDataStore
    {
        public DataStore()
        {
            _deviceService = DependencyService.Get<IDeviceService>();
            _headers = new Dictionary<string, string>()
            {
                [Constants.HeaderAppName] = _deviceService.AppName,
                [Constants.HeaderBundleClient] = _deviceService.AppBundle,
                [Constants.HeaderCompressedResult] = true.ToString(),
                [Constants.HeaderOs] = _deviceService.Os,
                [Constants.HeaderDeviceId] = _deviceService.DeviceId,
                [Constants.HeaderPhone] = _deviceService.DeviceName,
                [Constants.HeaderAppVersion] = _deviceService.AppVersion,
                [Constants.HeaderUserId] = 0.ToString(),
            };
        }

        public async Task<UserInfo> Auth(string personnelNumber)
        {
            Debug.WriteLine($"{nameof(Auth)}: start");
            //personnelNumber = "1234";
            var responseInfo = await RequestServiceHelper.Post($"{Globals.BaseUrl}/Auth",
                new AuthArgument() { PersonnelNumber = personnelNumber }, _headers);
            if (responseInfo.IsSuccess)
            {
                var userInfo = ZippedJsonHelper.GetObjectFromZippedString<UserInfo>(responseInfo.Content);
                Globals.CurrentUser = userInfo;
                _headers[Constants.HeaderUserId] = userInfo.UserId.ToString();
                Debug.WriteLine($"{nameof(Auth)}: finish");
                return userInfo;
            }
            throw new AuthenticationException(responseInfo.Content);
        }

        public string ConnectionErrorString => @"Проблема со связью
Не получается связаться с внешним сервисом. Проверьте настройки интернета и повторите попытку.";

        private List<EnterpriseInfo> _enterprises = new List<EnterpriseInfo>();
        private List<NewsBlockInfo> _newsBlocks = new List<NewsBlockInfo>();
        private List<AnswerInfo> _answers = new List<AnswerInfo>();
        private List<SurveyReportInfo> _surveyReports = new List<SurveyReportInfo>();

        private IDeviceService _deviceService;

        private Dictionary<string, string> _headers;

        public async Task<List<EnterpriseInfo>> GetEnterprises(bool forceRefresh = false)
        {
            return await DoRequest<EnterpriseInfo>("Enterprises", null, forceRefresh);
        }

        public async Task<List<SurveyReportInfo>> GetReports(bool forceRefresh = false)
        {
            return await DoRequest<SurveyReportInfo>("SurveyReports", new SurveyReportInfo[] { }, forceRefresh);
        }

        public ImageSource GetImageSource(string imageUrl)
        {
            if (imageUrl.IsNullOrEmpty())
            {
                return null;
            }

            if (Application.Current.Properties.ContainsKey(imageUrl))
            {
                return ImageSource.FromStream(() => new MemoryStream((byte[]) Application.Current.Properties[imageUrl]));
            }

            try
            {
                var imageBuf = RequestServiceHelper.DownloadDataFromWebMethod(imageUrl, out var responseHeaders, _headers);
                Application.Current.Properties[imageUrl] = imageBuf;
                Task.Run(async () => await Application.Current.SavePropertiesAsync());
                return ImageSource.FromStream(() => new MemoryStream(imageBuf));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(GetImageSource)} ({imageUrl}): {ex}");
            }

            return null;
        }

        private async Task<List<T>> DoRequest<T>(string methodName, object model, bool forceRefresh) where T: class
        {
            var list = typeof(T) == typeof(EnterpriseInfo)
                ? _enterprises
                : typeof(T) == typeof(NewsBlockInfo)
                    ? _newsBlocks
                    : typeof(T) == typeof(AnswerInfo)
                        ? _answers
                        : typeof(T) == typeof(SurveyReportInfo)
                            ? (IList) _surveyReports
                            : throw new ArgumentException($"{nameof(DoRequest)}: invalid type: {typeof(T)}");

            Debug.WriteLine($"{nameof(DoRequest)}: start getting {typeof(T).Name}");

            if (forceRefresh || list.Count == 0)
            {
                try
                {
                    var postResult = await RequestServiceHelper.Post($"{Globals.BaseUrl}/{methodName}", model ?? new object(), _headers);
                    if (postResult.IsSuccess)
                    {
                        var resList = ZippedJsonHelper.GetObjectFromZippedString<List<T>>(postResult.Content, JsonConverters.AllConverters);
                        list.Clear();
                        resList.ForEach(item => list.Add(item));
                    }
                    else
                    {
                        Debug.WriteLine($"{nameof(DoRequest)}: error getting type {typeof(T)}: {postResult.Content}");
                        throw new PreDefinedException(this, "Ошибка при обращении к серверу");
                    }
                }
                catch (WebException)
                {
                    throw new Exception(ConnectionErrorString);
                }
            }

            Debug.WriteLine($"{nameof(DoRequest)}: returning {typeof(T).Name}");
            return (List<T>)list;
        }

        public async Task<List<NewsBlockInfo>> GetNews(bool forceRefresh = false)
        {
            return await DoRequest<NewsBlockInfo>("NewsBlocks", null, forceRefresh);
        }

        private readonly Regex _fileNameRegex = new Regex("filename=(.*);");
        public int UnpackTime { get; set; }

        public async Task<string> GetApk(int apkId, Action<double> progressChangedAction = null)
        {
            UnpackTime = 0;
            DurationHelper.InitTiming("GetApk");
            Dictionary<string, string> responseHeaders;
            byte[] bytes;

            var postResult = await RequestServiceHelper.Get($"{Globals.BaseUrl}/GetApkZipped/{apkId}", _headers,
                progressChangedAction);
            if (postResult.IsSuccess)
            {
                bytes = postResult.BinaryContent;
                responseHeaders = postResult.Headers;
            }
            else
            {
                throw new WebException(postResult.Content);
            }

            Debug.WriteLine($"GetApk: downloaded ({DurationHelper.GetSecondsString("GetApk")})");

            var start = DateTime.Now;
            bytes = ZippedJsonHelper.GetUnzippedArray(bytes);
            UnpackTime = (int) (DateTime.Now - start).TotalMilliseconds;
            Debug.WriteLine($"GetApk: unzipped ({DurationHelper.GetSecondsString("GetApk")})");

            string storagePath = _deviceService.GetExternalStorage();
            var headerValue = responseHeaders.First(h => _fileNameRegex.IsMatch(h.Value)).Value;
            if (headerValue.IsNullOrEmpty())
            {
                throw new WebException("File not found");
            }

            var localFilename = _fileNameRegex.Match(headerValue).Groups[1].Value;
            string localPath = Path.Combine(storagePath, localFilename);
            await Task.Run(() => File.WriteAllBytes(localPath, bytes));
            Debug.WriteLine($"GetApk: saved file {localPath} ({DurationHelper.GetSecondsString("GetApk")})");
            return localPath;
        }
    }
}
