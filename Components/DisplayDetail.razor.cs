using BzVault.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzVault.Components
{
    public class DisplayDetailBase : ComponentBase
    {
        [Parameter]
        public ApiLoginData Record { get; set; }
        [Parameter]
        public EventCallback<int> OnProcessDelete { get; set; }
        [Parameter]
        public EventCallback<int> OnProcessEdit { get; set; }
    }
}
