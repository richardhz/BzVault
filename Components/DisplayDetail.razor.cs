using BzVault.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzVault.Components
{
    public class DisplayDetailBase : ComponentBase
    {
        [Inject] IDialogService DialogService { get; set; }
        [Parameter]
        public ApiLoginData Record { get; set; }
        [Parameter]
        public EventCallback<int> OnProcessDelete { get; set; }
        [Parameter]
        public EventCallback<int> OnProcessEdit { get; set; }

        protected async Task OpenDeleteDialog()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to delete {Record.Name} record? This process cannot be undone.");
            parameters.Add("ButtonText", $"Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, DisableBackdropClick = true };

            var dialog = DialogService.Show<Confirmation_Dialog>($"Delete  {Record.Name}", parameters, options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var str = $"Deleting {Record.Name}";
                Console.WriteLine(str);
            }

        }
    }
}
