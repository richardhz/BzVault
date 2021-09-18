using BzVault.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzVault.Components
{
    public class DisplayItemBase : ComponentBase
    {
        [Parameter]
        public LoginItem Record { get; set; }
        
    }
}
