using BzVault.Models;
using BzVault.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzVault.Pages
{
    public class VaultBase : ComponentBase
    {
        [Inject] IDataService DataService { get; set; }

        protected IEnumerable<LoginItem> Data { get; set; }
        protected Link Next { get; set; }
        protected Link Prev { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var data = await DataService.GetLogins(1);

            Data = data.Records;
            Next = data.Links.FirstOrDefault(l => l.Rel == "nextPage");
            Prev = data.Links.FirstOrDefault(l => l.Rel == "previousPage");
            
        }
    }
}
