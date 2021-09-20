using BzVault.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BzVault.Components
{
    public class DisplayItemListBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<LoginItem> Records { get; set; }
        [Parameter]
        public EventCallback<int> OnClick { get; set; }


        

        //private async Task GetDetail(string queryString)
        //{
        //    var qData = HttpUtility.ParseQueryString(queryString);
        //    if (int.TryParse(qData["PageNumber"], out int page))
        //    {
        //        await OnClick.InvokeAsync(page);
        //    }
        //}
    }
}
