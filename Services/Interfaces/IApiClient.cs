using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BzVault.Services.Interfaces
{
    public interface IApiClient
    {
        public string BaseAddress { get; set; }
        public string Name { get; set; }
        public HttpStatusCode? Status { get; set; }
        public string ErrorMessage { get; set; }
        Task<T> GetAsync<T, TV>(string endpoint, TV value = default) where T : class;
        Task<T> GetAsync<T>(string endpoint) where T : class;
        //string ToQueryString(object model);
        Task<T> PostAsync<T, TV>(string endpoint, TV value) where T : class;
        HttpClient CreateApiClient();
    }
}
