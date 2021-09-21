using BzVault.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BzVault.Components
{
    public class DisplayItemListBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<LoginItem> Records { get; set; }
        [Parameter]
        public EventCallback<Guid> OnClickItem { get; set; }
        
        protected async Task DoDetail(Guid id)
        {
            await OnClickItem.InvokeAsync(id);
        }
    }
}
