using BzVault.Models;
using BzVault.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BzVault.Services
{
    public class DataService : IDataService
    {
        private readonly IApiClient _client;

        private readonly JsonSerializerOptions jso = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public DataService(IApiClient client)
        {
            _client = client;
        }

        //public async Task<LoginListMeta> GetLogins(int? page = 1)
        //{

        //    var data = await _client.GetAsync<LoginListMeta, string>($"list?pageNumber={page} &OrderBy=name");
        //    return data;
        //}

        //public async Task<ApiLoginData> GetDetail(Guid id)
        //{
        //    var client = _clientFactory.CreateClient("RemoteApi");
        //    var data = await client.GetFromJsonAsync<ApiLoginData>($"list/{id}", jso).ConfigureAwait(false);
        //    return data;
        //}

        public async Task<ApiLoginDataRecord> GetDetailRecord(Guid id)
        {
            var data = await _client.GetAsync<ApiLoginDataRecord>($"list/{id}").ConfigureAwait(false);
            return data;
        }

        public async Task<HttpResponseMessage> UpdateDetailRecord(ApiLoginDataRecord record)
        {
            var client = _client.CreateApiClient();
            var id = record.Id;
            var data = await client.PutAsJsonAsync<ApiLoginDataRecord>($"{id}", record);
            return data;
        }

        //public async Task<ApiLoginDataRecord> CreateRecord(ApiLoginDataRecord record)
        //{
        //    var client = _clientFactory.CreateClient("RemoteApi");
        //    var data = await client.PostAsJsonAsync<ApiLoginDataRecord>("",record);
        //    //var x = data.
        //}

        public async Task<string> DeleteLogins(Guid id)
        {
            var client = _client.CreateApiClient();
            var data = await client.DeleteAsync($"{id}").ConfigureAwait(false);
            return data.StatusCode.ToString();
        }



        //private async Task<LoginListMeta> GetJsonDataAsync(string endpoint)
        //{
        //    LoginListMeta data = null;
        //    var client = _clientFactory.CreateClient("RemoteApi");
        //    try
        //    {
        //        data = await client.GetFromJsonAsync<LoginListMeta>(endpoint, jso).ConfigureAwait(false);
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        if (data is not null)
        //        {
        //            data.ErrorMessage = ex.StatusCode.ToString() + " ERROR";
        //        }

        //    }
        //    catch (NotSupportedException ex)
        //    {
        //        if (data is not null)
        //        {
        //            data.ErrorMessage = ex.Message + " ERROR";
        //        }
        //    }

        //    catch (JsonException ex)
        //    {
        //        if (data is not null)
        //        {
        //            data.ErrorMessage = ex.Message + " ERROR";
        //        }
        //    }
        //    return data;
        //}
    }
}
