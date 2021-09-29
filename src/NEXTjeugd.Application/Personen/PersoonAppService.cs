using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using NEXTjeugd.Permissions;
using NEXTjeugd.Personen;

namespace NEXTjeugd.Personen
{

    [Authorize(NEXTjeugdPermissions.Personen.Default)]
    public class PersonenAppService : ApplicationService, IPersonenAppService
    {
        private readonly IPersoonRepository _persoonRepository;

        public PersonenAppService(IPersoonRepository persoonRepository)
        {
            _persoonRepository = persoonRepository;
        }

        public virtual async Task<PagedResultDto<PersoonDto>> GetListAsync(GetPersonenInput input)
        {
            var totalCount = await _persoonRepository.GetCountAsync(input.FilterText, input.Roepnaam, input.Voorletters, input.Tussenvoegsel, input.Achternaam, input.BSN, input.Geslacht, input.GeboortedatumMin, input.GeboortedatumMax, input.Geboorteland, input.Type);
            var items = await _persoonRepository.GetListAsync(input.FilterText, input.Roepnaam, input.Voorletters, input.Tussenvoegsel, input.Achternaam, input.BSN, input.Geslacht, input.GeboortedatumMin, input.GeboortedatumMax, input.Geboorteland, input.Type, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PersoonDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Persoon>, List<PersoonDto>>(items)
            };
        }

        public virtual async Task<PersoonDto> GetAsync(int id)
        {
            return ObjectMapper.Map<Persoon, PersoonDto>(await _persoonRepository.GetAsync(id));
        }

        [Authorize(NEXTjeugdPermissions.Personen.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _persoonRepository.DeleteAsync(id);
        }

        [Authorize(NEXTjeugdPermissions.Personen.Create)]
        public virtual async Task<PersoonDto> CreateAsync(PersoonCreateDto input)
        {

            var persoon = ObjectMapper.Map<PersoonCreateDto, Persoon>(input);
            persoon.TenantId = CurrentTenant.Id;
            persoon = await _persoonRepository.InsertAsync(persoon, autoSave: true);
            return ObjectMapper.Map<Persoon, PersoonDto>(persoon);
        }

        [Authorize(NEXTjeugdPermissions.Personen.Edit)]
        public virtual async Task<PersoonDto> UpdateAsync(int id, PersoonUpdateDto input)
        {

            var persoon = await _persoonRepository.GetAsync(id);
            ObjectMapper.Map(input, persoon);
            persoon = await _persoonRepository.UpdateAsync(persoon, autoSave: true);
            return ObjectMapper.Map<Persoon, PersoonDto>(persoon);
        }
    }
}