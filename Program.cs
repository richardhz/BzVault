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
using BlazorState;
using System.Reflection;

namespace BzVault
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.Configure<WebApiOptions>(builder.Configuration.GetSection(nameof(WebApiOptions)));
            var settings = builder.Configuration.GetSection(nameof(WebApiOptions)).Get<WebApiOptions>();
            builder.Services.AddHttpClient(settings.NamedClient,
                cli =>
                {
                    cli.BaseAddress = new Uri(settings.BaseAddress);
                });

            builder.Services.AddBlazorState
              (
                (aOptions) =>

                  aOptions.Assemblies =
                  new Assembly[]
                  {
                    typeof(Program).GetTypeInfo().Assembly,
                  }
              );
            builder.Services.AddScoped<IDataService, DataService>();
            builder.Services.AddScoped<IApiClient,ApiClient>();
            builder.Services.AddMudServices();
            await builder.Build().RunAsync();
        }
    }
}
