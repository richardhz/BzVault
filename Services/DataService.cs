﻿using BzVault.Models;
using BzVault.Services.Interfaces;
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
        private readonly IHttpClientFactory _clientFactory;

        private readonly JsonSerializerOptions jso = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public DataService(IHttpClientFactory factory)
        {
            _clientFactory = factory;
        }

        public async Task<LoginListMeta> GetLogins(int page = 1)
        {
            var data = await GetJsonDataAsync($"list?pageNumber={page} &OrderBy=name");
            return data;
        }

        private async Task<LoginListMeta> GetJsonDataAsync(string endpoint)
        {
            LoginListMeta data = null;
            var client = _clientFactory.CreateClient("RemoteApi");
            try
            {
                data = await client.GetFromJsonAsync<LoginListMeta>(endpoint, jso).ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                if (data is not null)
                {
                    data.ErrorMessage = ex.StatusCode.ToString() + " ERROR";
                }

            }
            catch (NotSupportedException ex)
            {
                if (data is not null)
                {
                    data.ErrorMessage = ex.Message + " ERROR";
                }
            }

            catch (JsonException ex)
            {
                if (data is not null)
                {
                    data.ErrorMessage = ex.Message + " ERROR";
                }
            }
            return data;
        }
    }
}
