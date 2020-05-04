using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InquirerForAndroid.Models;

namespace InquirerForAndroid.Services
{
    public interface IDataStore
    {
        Task<UserInfo> Auth(string personnelNumber);
        Task<List<EnterpriseInfo>> GetEnterprises(bool forceRefresh);
        Task<List<NewsBlockInfo>> GetNews(bool forceRefresh);
        Task<string> GetApk(int apkId, Action<double> progressChangedAction = null);
        int UnpackTime { get; set; }
        string ConnectionErrorString { get; }
    }
}
