using BzVault.Models;
using BzVault.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Hosting;
using BlazorState;
using BzVault.Features.Login;

namespace BzVault.Pages
{
    public class VaultBase : BlazorStateComponent
    {
        LoginState LoginState => GetState<LoginState>();
        private LoginListMeta ListData => LoginState.LoginData;
        [Inject] IDataService DataService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        protected IList<LoginItem> Data { get; set; }
        protected ApiLoginDataRecord Record { get; set; }
        protected Link Next { get; set; }
        protected Link Prev { get; set; }
        protected int? Page { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (ListData.Records is null)
            {
                Page = 1;
                await GetDataAsync();
            }
            PopulateData();
        }

        private async Task GetDataAsync()
        {
            await Mediator.Send(new LoginState.LoginStatePage { RequestedPage = Page.Value });
        }

        private void PopulateData()
        {
            if (ListData is not null)
            {
                Data = ListData.Records.ToList();
                Next = ListData.Links.FirstOrDefault(l => l.Rel == "nextPage");
                Prev = ListData.Links.FirstOrDefault(l => l.Rel == "previousPage");
            }
        }


        private async Task GetRequestedPage(string queryString)
        {
            var qData = HttpUtility.ParseQueryString(queryString);
            if (int.TryParse(qData["PageNumber"], out int page))
            {
                Data = null;
                Page = page;
                await GetDataAsync();
                PopulateData();
                Record = null;
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

        // Other data actions===================================================
        protected  async void GetDetail(Guid id)
        {
            Record = await DataService.GetDetailRecord(id);
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


        protected async void EditRecord(ApiLoginDataRecord record)
        {
            Snackbar.Add($"Updating {record.Name} ", Severity.Info);
            var status = await DataService.UpdateDetailRecord(record);
            if(status.IsSuccessStatusCode && status.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Snackbar.Add($"{record.Name} Updated", Severity.Info);
                Record = record;
                StateHasChanged();
            }
        }
        // Other data actions===================================================End


       
    }
}
