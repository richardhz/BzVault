using BzVault.Models;
using BzVault.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BzVault.Services
{
    public class ApiClient : IApiClient
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly WebApiOptions _settings;
        private readonly JsonSerializerOptions jsOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public string BaseAddress { get; set; }
        public string Name { get; set; }
        public HttpStatusCode? Status { get; set; }
        public string ErrorMessage { get; set; }

        public ApiClient(IHttpClientFactory factory, IOptions<WebApiOptions> apiSettings)
        {
            _httpClientFactory = factory;
            _settings = apiSettings.Value;
            
            if (!string.IsNullOrWhiteSpace(_settings.NamedClient))
            {
                Name = _settings.NamedClient;
            }
            else
            {
                BaseAddress = _settings.BaseAddress;
            }

            ErrorMessage = string.Empty;
            Status = HttpStatusCode.OK;
        }






        public async Task<TReturn> GetAsync<TReturn>(string endpoint)
             where TReturn : class
        {
            return await GetAsync<TReturn, int>(endpoint);
        }

        public async Task<TReturn> GetAsync<TReturn, TParam>(string endpoint, TParam value = default)
            where TReturn : class
        {
            HttpClient client = CreateApiClient(); ;
            TReturn data = default;
            if (client is null)
            {
                return data;
            }
            //if (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(BaseAddress))
            //{
            //    ErrorMessage = "Base address or Name not provided";
            //    Status = HttpStatusCode.Gone;
            //    return data;
            //}

            //if (!string.IsNullOrWhiteSpace(Name))
            //{
            //    client = _httpClientFactory.CreateClient(Name);
            //}
            //else
            //{
            //    client = _httpClientFactory.CreateClient();
            //    client.BaseAddress = new Uri(BaseAddress);
            //}

            try
            {
                if (EqualityComparer<TParam>.Default.Equals(value, default(TParam)))
                {
                    data = await client.GetFromJsonAsync<TReturn>(endpoint, jsOptions);
                }
                else
                {
                    data = await client.GetFromJsonAsync<TReturn>($"{ endpoint}/{value}", jsOptions);
                }


            }
            catch (HttpRequestException ex)
            {
                Status = ex.StatusCode;
                ErrorMessage = ex.Message;
                data = default;
            }
            catch (NotSupportedException ex)
            {
                ErrorMessage = ex.Message;
                Status = HttpStatusCode.InternalServerError;
                data = default;
            }
            //catch (JsonException ex)
            //{
            //    Message = ex.Message;
            //    Status = HttpStatusCode.BadRequest;
            //    data = default;
            //}
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Status = HttpStatusCode.InternalServerError;
                data = default;
            }

            return data;
        }


        public async Task<TReturn> PostAsync<TReturn, TParam>(string endpoint, TParam value)
            where TReturn : class
        {
            TReturn data = default;
            HttpClient client = CreateApiClient();
            if (client == null)
            {
                return data;
            }

            var json = JsonSerializer.Serialize(value);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode == true)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (responseContent != string.Empty)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    data = JsonSerializer.Deserialize<TReturn>(responseContent, options);
                }
            }
            else
            {
                data = default;
                ErrorMessage = response.ReasonPhrase.ToString();
                Status = HttpStatusCode.InternalServerError;
            }

            return data;
        }

        public HttpClient CreateApiClient()
        {
            HttpClient client = null;

            if (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(BaseAddress))
            {
                ErrorMessage = "Base address or Named Client not provided";
                Status = HttpStatusCode.InternalServerError;
                return null;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                client = _httpClientFactory.CreateClient(Name);
            }
            else
            {
                client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(BaseAddress);
            }

            return client;
        }

        //public string ToQueryString(object model)
        //{
        //    //var serialized = JsonSerializer.Serialize(model);
        //    //var deserialized = JsonSerializer.Deserialize<Dictionary<string, string>>(serialized);
        //    var data = ToKeyValue(model);
        //    var formUrlEncodedContent = new FormUrlEncodedContent(data);
        //    var urlEncodedString = formUrlEncodedContent.ReadAsStringAsync().Result;

        //    //var result = deserialized.Select((kvp) => kvp.Key.ToString() + "=" + Uri.EscapeDataString(kvp.Value)).Aggregate((p1,p2) => p1 + "&" + p2);
        //    return $"?{urlEncodedString}";

        //}


        //private IDictionary<string, string> ToKeyValue(object metaToken)
        //{
        //    if (metaToken == null)
        //    {
        //        return null;
        //    }


        //    JToken token = metaToken as JToken;
        //    if (token == null)
        //    {
        //        return ToKeyValue(JObject.FromObject(metaToken));
        //    }

        //    if (token.HasValues)
        //    {
        //        var contentData = new Dictionary<string, string>();
        //        foreach (var child in token.Children().ToList())
        //        {
        //            var childContent = ToKeyValue(child);
        //            if (childContent != null)
        //            {
        //                contentData = contentData.Concat(childContent)
        //                                         .ToDictionary(k => k.Key, v => v.Value);
        //            }
        //        }

        //        return contentData;
        //    }

        //    var jValue = token as JValue;
        //    if (jValue?.Value == null)
        //    {
        //        return null;
        //    }

        //    var value = jValue?.Type == JTokenType.Date ?
        //                    jValue?.ToString("o", CultureInfo.InvariantCulture) :
        //                    jValue?.ToString(CultureInfo.InvariantCulture);

        //    return new Dictionary<string, string> { { token.Path, value } };
        //}

    }
}
