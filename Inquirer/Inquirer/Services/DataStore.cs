using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using InquirerForAndroid.Models;
using InquirerForAndroid.Services;
using Rcn.Common;
using Rcn.Common.ExtensionMethods;
using Rcn.Common.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataStore))]
namespace InquirerForAndroid.Services
{
    public class DataStore : IDataStore
    {
        public UserInfo Auth(string login, string password, string pin)
        {
            return new UserInfo();
        }

        public string LastError { get; private set; }

        public string ConnectionErrorString => @"Проблема со связью
Не получается связаться с внешним сервисом. Проверьте настройки интернета и повторите попытку.";

        private List<EnterpriseInfo> _enterprises = new List<EnterpriseInfo>();

        private bool _nowRefreshing;

        public async Task<List<EnterpriseInfo>> GetEnterprises(bool forceRefresh)
        {
            if (forceRefresh && _nowRefreshing)
            {
                while (_nowRefreshing)
                {
                    await Task.Delay(20);
                }
                if (!LastError.IsNullOrEmpty())
                {
                    throw new Exception(LastError);
                }
                forceRefresh = false;
            }
            if (forceRefresh)
            {
                try
                {
                    _nowRefreshing = true;
                    LastError = "";

                    var res = await Task.Run(() => { return ""; });

                    //if (res.StartsWith(WebConstants.ErrorPrefix))
                    //{
                    //    LastError = res.Replace(WebConstants.ErrorPrefix, "");
                    //    throw new Exception(LastError);
                    //}

                    LastError = "";
                }
                catch (WebException ex)
                {
                    throw new Exception(ConnectionErrorString);
                }
                finally
                {
                    _nowRefreshing = false;
                }
            }
            return _enterprises;
        }
    }
}
