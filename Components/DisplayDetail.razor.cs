using BzVault.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace BzVault.Components
{
    public class DisplayDetailBase : ComponentBase
    {
        [Inject] IDialogService DialogService { get; set; }
        [Parameter]
        public ApiLoginDataRecord Record { get; set; }
        [Parameter]
        public EventCallback<Guid> OnProcessDelete { get; set; }
        [Parameter]
        public EventCallback<ApiLoginDataRecord> OnProcessEdit { get; set; }

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
                await OnProcessDelete.InvokeAsync(Record.Id);

            }

        }

        protected async Task OpenEditDialog()
        {
            ApiLoginData recordToEdit = new ApiLoginData
            {
                Id = Record.Id,
                Description = Record.Description,
                Login = Record.Login,
                Name = Record.Name,
                Password = Record.Password,
                Url = Record.Url
            };

            var parameters = new DialogParameters { ["Record"] = recordToEdit };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, DisableBackdropClick = true };

            var dialog = DialogService.Show<Edit_Dialog>("Edit ", parameters, options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                //this is where we create a new record.
                ApiLoginDataRecord newRecord = new ApiLoginDataRecord
                {
                    Id = recordToEdit.Id,
                    Description = recordToEdit.Description,
                    Login = recordToEdit.Login,
                    Name = recordToEdit.Name,
                    Password = recordToEdit.Password,
                    Url = recordToEdit.Url
                };

                if (newRecord != Record)
                {
                    await OnProcessEdit.InvokeAsync(newRecord);
                }

                
            }
        }
    }
}
