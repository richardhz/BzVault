using BzVault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BzVault.Services.Interfaces
{
    interface IDataService
    {
        //Task<LoginListMeta> GetLogins(int? page = 1);
        Task<HttpResponseMessage> UpdateDetailRecord(ApiLoginDataRecord record);
        Task<ApiLoginDataRecord> GetDetailRecord(Guid id);
        Task<string> DeleteLogins(Guid id);
    }
}
