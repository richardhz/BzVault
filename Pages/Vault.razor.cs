using BzVault.Models;
using BzVault.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BzVault.Pages
{
    public class VaultBase : ComponentBase
    {
        [Inject] IDataService DataService { get; set; }

        protected IEnumerable<LoginItem> Data { get; set; }
        protected Link Next { get; set; }
        protected Link Prev { get; set; }
        protected int? Page { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (Page == null)
            {
                Page = 1;
            }

            await GetDataAsync();
        }

        private async Task GetDataAsync()
        {
            var data = await DataService.GetLogins(Page);

            Data = data.Records;
            Next = data.Links.FirstOrDefault(l => l.Rel == "nextPage");
            Prev = data.Links.FirstOrDefault(l => l.Rel == "previousPage");
        }


        private async Task GetRequestedPage(string queryString)
        {
            var qData = HttpUtility.ParseQueryString(queryString);
            if (int.TryParse(qData["PageNumber"], out int page))
            {
                Data = null;
                Page = page;
                await GetDataAsync();
            }
        }


        private async Task GetDetailAsync()
        {
            //not yet implemented
            var data = await DataService.GetLogins(Page);
        }


        protected async void GetDetail()
        {
            await GetDataAsync();
        }


        protected static bool ButtonIsDisabled(Link item)
        {
            return (item == null);
        }

        protected async Task GotoNextPage()
        {
            var queryString = new Uri(Next.Href).Query;
            await GetRequestedPage(queryString);

        }

        protected async Task GotoPrevPage()
        {
            var queryString = new Uri(Prev.Href).Query;
            await GetRequestedPage(queryString);

        }

       
    }
}
