﻿using BzVault.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzVault.Components
{
    public class Edit_DialogBase : ComponentBase
    {
       
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public ApiLoginData Record { get; set; } = new();
        protected string Password2 { get; set; }


        protected void Cancel() => MudDialog.Cancel();
        protected void Submit() => MudDialog.Close(DialogResult.Ok(true));
        
    }
}