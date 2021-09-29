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
using NEXTjeugd.Personen;
using NEXTjeugd.Permissions;
using NEXTjeugd.Shared;

namespace NEXTjeugd.Blazor.Pages
{
    public partial class Personen
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<PersoonDto> PersoonList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreatePersoon { get; set; }
        private bool CanEditPersoon { get; set; }
        private bool CanDeletePersoon { get; set; }
        private PersoonCreateDto NewPersoon { get; set; }
        private Validations NewPersoonValidations { get; set; }
        private PersoonUpdateDto EditingPersoon { get; set; }
        private Validations EditingPersoonValidations { get; set; }
        private int EditingPersoonId { get; set; }
        private Modal CreatePersoonModal { get; set; }
        private Modal EditPersoonModal { get; set; }
        private GetPersonenInput Filter { get; set; }
        private DataGridEntityActionsColumn<PersoonDto> EntityActionsColumn { get; set; }
        
        public Personen()
        {
            NewPersoon = new PersoonCreateDto();
            EditingPersoon = new PersoonUpdateDto();
            Filter = new GetPersonenInput
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
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Personen"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewPersoon"], () =>
            {
                OpenCreatePersoonModal();
                return Task.CompletedTask;
            }, IconName.Add, requiredPolicyName: NEXTjeugdPermissions.Personen.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreatePersoon = await AuthorizationService
                .IsGrantedAsync(NEXTjeugdPermissions.Personen.Create);
            CanEditPersoon = await AuthorizationService
                            .IsGrantedAsync(NEXTjeugdPermissions.Personen.Edit);
            CanDeletePersoon = await AuthorizationService
                            .IsGrantedAsync(NEXTjeugdPermissions.Personen.Delete);
        }

        private async Task GetPersonenAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await PersonenAppService.GetListAsync(Filter);
            PersoonList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetPersonenAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<PersoonDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetPersonenAsync();
            await InvokeAsync(StateHasChanged);
        }

        private void OpenCreatePersoonModal()
        {
            NewPersoon = new PersoonCreateDto{
                Geboortedatum = DateTime.Now,

                
            };
            NewPersoonValidations.ClearAll();
            CreatePersoonModal.Show();
        }

        private void CloseCreatePersoonModal()
        {
            CreatePersoonModal.Hide();
        }

        private void OpenEditPersoonModal(PersoonDto input)
        {
            EditingPersoonId = input.Id;
            EditingPersoon = ObjectMapper.Map<PersoonDto, PersoonUpdateDto>(input);
            EditingPersoonValidations.ClearAll();
            EditPersoonModal.Show();
        }

        private async Task DeletePersoonAsync(PersoonDto input)
        {
            await PersonenAppService.DeleteAsync(input.Id);
            await GetPersonenAsync();
        }

        private async Task CreatePersoonAsync()
        {
            await PersonenAppService.CreateAsync(NewPersoon);
            await GetPersonenAsync();
            CreatePersoonModal.Hide();
        }

        private void CloseEditPersoonModal()
        {
            EditPersoonModal.Hide();
        }

        private async Task UpdatePersoonAsync()
        {
            await PersonenAppService.UpdateAsync(EditingPersoonId, EditingPersoon);
            await GetPersonenAsync();
            EditPersoonModal.Hide();
        }

    }
}
