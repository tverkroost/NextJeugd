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
using NEXTjeugd.Adressen;

namespace NEXTjeugd.Adressen
{

    [Authorize(NEXTjeugdPermissions.Adressen.Default)]
    public class AdressenAppService : ApplicationService, IAdressenAppService
    {
        private readonly IAdresRepository _adresRepository;

        public AdressenAppService(IAdresRepository adresRepository)
        {
            _adresRepository = adresRepository;
        }

        public virtual async Task<PagedResultDto<AdresDto>> GetListAsync(GetAdressenInput input)
        {
            var totalCount = await _adresRepository.GetCountAsync(input.FilterText, input.Postcode, input.Straatnaam, input.Huisnummer, input.Woonplaats, input.Stadsdeel, input.BegindatumMin, input.BegindatumMax, input.Einddatum, input.Geheim);
            var items = await _adresRepository.GetListAsync(input.FilterText, input.Postcode, input.Straatnaam, input.Huisnummer, input.Woonplaats, input.Stadsdeel, input.BegindatumMin, input.BegindatumMax, input.Einddatum, input.Geheim, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AdresDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Adres>, List<AdresDto>>(items)
            };
        }

        public virtual async Task<AdresDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Adres, AdresDto>(await _adresRepository.GetAsync(id));
        }

        [Authorize(NEXTjeugdPermissions.Adressen.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _adresRepository.DeleteAsync(id);
        }

        [Authorize(NEXTjeugdPermissions.Adressen.Create)]
        public virtual async Task<AdresDto> CreateAsync(AdresCreateDto input)
        {

            var adres = ObjectMapper.Map<AdresCreateDto, Adres>(input);
            adres.TenantId = CurrentTenant.Id;
            adres = await _adresRepository.InsertAsync(adres, autoSave: true);
            return ObjectMapper.Map<Adres, AdresDto>(adres);
        }

        [Authorize(NEXTjeugdPermissions.Adressen.Edit)]
        public virtual async Task<AdresDto> UpdateAsync(Guid id, AdresUpdateDto input)
        {

            var adres = await _adresRepository.GetAsync(id);
            ObjectMapper.Map(input, adres);
            adres = await _adresRepository.UpdateAsync(adres, autoSave: true);
            return ObjectMapper.Map<Adres, AdresDto>(adres);
        }
    }
}