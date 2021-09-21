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

        protected IList<LoginItem> Data { get; set; }
        protected ApiLoginData Record { get; set; }
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

            Data = data.Records.ToList();
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
                Record = null;
            }
        }

        protected  async void GetDetail(Guid id)
        {
            Record = await DataService.GetDetail(id);
            StateHasChanged();
        }

        protected async void DeleteRecord(Guid id)
        {
            var status = await DataService.DeleteLogins(id);
            if (status == "OK")
            {
                var record = Data.Where(r => r.Id == id).Single();
                Data.Remove(record);
                Record = null;
                StateHasChanged();
            }
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
