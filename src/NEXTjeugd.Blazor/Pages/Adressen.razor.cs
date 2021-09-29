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
using NEXTjeugd.Adressen;
using NEXTjeugd.Permissions;
using NEXTjeugd.Shared;

namespace NEXTjeugd.Blazor.Pages
{
    public partial class Adressen
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<AdresDto> AdresList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateAdres { get; set; }
        private bool CanEditAdres { get; set; }
        private bool CanDeleteAdres { get; set; }
        private AdresCreateDto NewAdres { get; set; }
        private Validations NewAdresValidations { get; set; }
        private AdresUpdateDto EditingAdres { get; set; }
        private Validations EditingAdresValidations { get; set; }
        private Guid EditingAdresId { get; set; }
        private Modal CreateAdresModal { get; set; }
        private Modal EditAdresModal { get; set; }
        private GetAdressenInput Filter { get; set; }
        private DataGridEntityActionsColumn<AdresDto> EntityActionsColumn { get; set; }
        
        public Adressen()
        {
            NewAdres = new AdresCreateDto();
            EditingAdres = new AdresUpdateDto();
            Filter = new GetAdressenInput
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
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Adressen"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewAdres"], () =>
            {
                OpenCreateAdresModal();
                return Task.CompletedTask;
            }, IconName.Add, requiredPolicyName: NEXTjeugdPermissions.Adressen.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateAdres = await AuthorizationService
                .IsGrantedAsync(NEXTjeugdPermissions.Adressen.Create);
            CanEditAdres = await AuthorizationService
                            .IsGrantedAsync(NEXTjeugdPermissions.Adressen.Edit);
            CanDeleteAdres = await AuthorizationService
                            .IsGrantedAsync(NEXTjeugdPermissions.Adressen.Delete);
        }

        private async Task GetAdressenAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await AdressenAppService.GetListAsync(Filter);
            AdresList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetAdressenAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<AdresDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetAdressenAsync();
            await InvokeAsync(StateHasChanged);
        }

        private void OpenCreateAdresModal()
        {
            NewAdres = new AdresCreateDto{
                Begindatum = DateTime.Now,

                
            };
            NewAdresValidations.ClearAll();
            CreateAdresModal.Show();
        }

        private void CloseCreateAdresModal()
        {
            CreateAdresModal.Hide();
        }

        private void OpenEditAdresModal(AdresDto input)
        {
            EditingAdresId = input.Id;
            EditingAdres = ObjectMapper.Map<AdresDto, AdresUpdateDto>(input);
            EditingAdresValidations.ClearAll();
            EditAdresModal.Show();
        }

        private async Task DeleteAdresAsync(AdresDto input)
        {
            await AdressenAppService.DeleteAsync(input.Id);
            await GetAdressenAsync();
        }

        private async Task CreateAdresAsync()
        {
            await AdressenAppService.CreateAsync(NewAdres);
            await GetAdressenAsync();
            CreateAdresModal.Hide();
        }

        private void CloseEditAdresModal()
        {
            EditAdresModal.Hide();
        }

        private async Task UpdateAdresAsync()
        {
            await AdressenAppService.UpdateAsync(EditingAdresId, EditingAdres);
            await GetAdressenAsync();
            EditAdresModal.Hide();
        }

    }
}
