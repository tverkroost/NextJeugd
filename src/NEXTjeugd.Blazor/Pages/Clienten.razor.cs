using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using NEXTjeugd.Clienten;
using NEXTjeugd.Permissions;
using NEXTjeugd.Shared;

namespace NEXTjeugd.Blazor.Pages
{
    public partial class Clienten
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<ClientDto> ClientList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateClient { get; set; }
        private bool CanEditClient { get; set; }
        private bool CanDeleteClient { get; set; }
        private ClientCreateDto NewClient { get; set; }
        private Validations NewClientValidations { get; set; }
        private ClientUpdateDto EditingClient { get; set; }
        private Validations EditingClientValidations { get; set; }
        private int EditingClientId { get; set; }
        private Modal CreateClientModal { get; set; }
        private Modal EditClientModal { get; set; }
        private GetClientenInput Filter { get; set; }
        private DataGridEntityActionsColumn<ClientDto> EntityActionsColumn { get; set; }
        
        public Clienten()
        {
            NewClient = new ClientCreateDto();
            EditingClient = new ClientUpdateDto();
            Filter = new GetClientenInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Clienten"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewClient"], () =>
            {
                OpenCreateClientModal();
                return Task.CompletedTask;
            }, IconName.Add, requiredPolicyName: NEXTjeugdPermissions.Clienten.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateClient = await AuthorizationService
                .IsGrantedAsync(NEXTjeugdPermissions.Clienten.Create);
            CanEditClient = await AuthorizationService
                            .IsGrantedAsync(NEXTjeugdPermissions.Clienten.Edit);
            CanDeleteClient = await AuthorizationService
                            .IsGrantedAsync(NEXTjeugdPermissions.Clienten.Delete);
        }

        private async Task GetClientenAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await ClientenAppService.GetListAsync(Filter);
            ClientList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetClientenAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ClientDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetClientenAsync();
            await InvokeAsync(StateHasChanged);
        }

        private void OpenCreateClientModal()
        {
            NewClient = new ClientCreateDto{
                
                
            };
            NewClientValidations.ClearAll();
            CreateClientModal.Show();
        }

        private void CloseCreateClientModal()
        {
            CreateClientModal.Hide();
        }

        private void OpenEditClientModal(ClientDto input)
        {
            EditingClientId = input.Id;
            EditingClient = ObjectMapper.Map<ClientDto, ClientUpdateDto>(input);
            EditingClientValidations.ClearAll();
            EditClientModal.Show();
        }

        private async Task DeleteClientAsync(ClientDto input)
        {
            await ClientenAppService.DeleteAsync(input.Id);
            await GetClientenAsync();
        }

        private async Task CreateClientAsync()
        {
            await ClientenAppService.CreateAsync(NewClient);
            await GetClientenAsync();
            CreateClientModal.Hide();
        }

        private void CloseEditClientModal()
        {
            EditClientModal.Hide();
        }

        private async Task UpdateClientAsync()
        {
            await ClientenAppService.UpdateAsync(EditingClientId, EditingClient);
            await GetClientenAsync();
            EditClientModal.Hide();
        }

    }
}
