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
using NEXTjeugd.Jeugdigen;
using NEXTjeugd.Permissions;
using NEXTjeugd.Shared;

namespace NEXTjeugd.Blazor.Pages
{
    public partial class Jeugdigen
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<JeugdigeDto> JeugdigeList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateJeugdige { get; set; }
        private bool CanEditJeugdige { get; set; }
        private bool CanDeleteJeugdige { get; set; }
        private JeugdigeCreateDto NewJeugdige { get; set; }
        private Validations NewJeugdigeValidations { get; set; }
        private JeugdigeUpdateDto EditingJeugdige { get; set; }
        private Validations EditingJeugdigeValidations { get; set; }
        private int EditingJeugdigeId { get; set; }
        private Modal CreateJeugdigeModal { get; set; }
        private Modal EditJeugdigeModal { get; set; }
        private GetJeugdigenInput Filter { get; set; }
        private DataGridEntityActionsColumn<JeugdigeDto> EntityActionsColumn { get; set; }
        
        public Jeugdigen()
        {
            NewJeugdige = new JeugdigeCreateDto();
            EditingJeugdige = new JeugdigeUpdateDto();
            Filter = new GetJeugdigenInput
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
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Jeugdigen"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewJeugdige"], () =>
            {
                OpenCreateJeugdigeModal();
                return Task.CompletedTask;
            }, IconName.Add, requiredPolicyName: NEXTjeugdPermissions.Jeugdigen.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateJeugdige = await AuthorizationService
                .IsGrantedAsync(NEXTjeugdPermissions.Jeugdigen.Create);
            CanEditJeugdige = await AuthorizationService
                            .IsGrantedAsync(NEXTjeugdPermissions.Jeugdigen.Edit);
            CanDeleteJeugdige = await AuthorizationService
                            .IsGrantedAsync(NEXTjeugdPermissions.Jeugdigen.Delete);
        }

        private async Task GetJeugdigenAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await JeugdigenAppService.GetListAsync(Filter);
            JeugdigeList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetJeugdigenAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<JeugdigeDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetJeugdigenAsync();
            await InvokeAsync(StateHasChanged);
        }

        private void OpenCreateJeugdigeModal()
        {
            NewJeugdige = new JeugdigeCreateDto{
                
                
            };
            NewJeugdigeValidations.ClearAll();
            CreateJeugdigeModal.Show();
        }

        private void CloseCreateJeugdigeModal()
        {
            CreateJeugdigeModal.Hide();
        }

        private void OpenEditJeugdigeModal(JeugdigeDto input)
        {
            EditingJeugdigeId = input.Id;
            EditingJeugdige = ObjectMapper.Map<JeugdigeDto, JeugdigeUpdateDto>(input);
            EditingJeugdigeValidations.ClearAll();
            EditJeugdigeModal.Show();
        }

        private async Task DeleteJeugdigeAsync(JeugdigeDto input)
        {
            await JeugdigenAppService.DeleteAsync(input.Id);
            await GetJeugdigenAsync();
        }

        private async Task CreateJeugdigeAsync()
        {
            await JeugdigenAppService.CreateAsync(NewJeugdige);
            await GetJeugdigenAsync();
            CreateJeugdigeModal.Hide();
        }

        private void CloseEditJeugdigeModal()
        {
            EditJeugdigeModal.Hide();
        }

        private async Task UpdateJeugdigeAsync()
        {
            await JeugdigenAppService.UpdateAsync(EditingJeugdigeId, EditingJeugdige);
            await GetJeugdigenAsync();
            EditJeugdigeModal.Hide();
        }

    }
}
