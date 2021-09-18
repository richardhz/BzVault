using BzVault.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzVault.Components
{
    public class DisplayItemListBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<LoginItem> Records { get; set; }
        [Parameter]
        public Link Prev { get; set; }
        [Parameter]
        public Link Next { get; set; }
    }
}
