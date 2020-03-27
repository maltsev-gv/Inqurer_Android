using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InquirerForAndroid.Models;

namespace InquirerForAndroid.Services
{
    public interface IDataStore
    {
        UserInfo Auth(string login, string password, string pin);
        Task<List<EnterpriseInfo>> GetEnterprises(bool forceRefresh);
    }
}
