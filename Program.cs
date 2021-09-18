using BzVault.Models;
using BzVault.Services;
using BzVault.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MudBlazor.Services;

namespace BzVault
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddHttpClient("RemoteApi",
                cli =>
                {
                    var settings = builder.Configuration.GetSection("RhzSettings").Get<RhzSettings>();
                    cli.BaseAddress = new Uri(settings.BaseUrl);
                });
            builder.Services.AddScoped<IDataService, DataService>();
            builder.Services.AddMudServices();
            await builder.Build().RunAsync();
        }
    }
}
