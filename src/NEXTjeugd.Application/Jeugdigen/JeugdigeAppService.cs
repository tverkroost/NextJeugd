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
using NEXTjeugd.Jeugdigen;

namespace NEXTjeugd.Jeugdigen
{

    [Authorize(NEXTjeugdPermissions.Jeugdigen.Default)]
    public class JeugdigenAppService : ApplicationService, IJeugdigenAppService
    {
        private readonly IJeugdigeRepository _jeugdigeRepository;

        public JeugdigenAppService(IJeugdigeRepository jeugdigeRepository)
        {
            _jeugdigeRepository = jeugdigeRepository;
        }

        public virtual async Task<PagedResultDto<JeugdigeDto>> GetListAsync(GetJeugdigenInput input)
        {
            var totalCount = await _jeugdigeRepository.GetCountAsync(input.FilterText, input.WoonsituatieMin, input.WoonsituatieMax, input.SamenstellingHuishoudenMin, input.SamenstellingHuishoudenMax, input.ToestemmingInformatiedeling, input.Notitie, input.Werkaantekening, input.InzageEigenDossier, input.GeheimDossier, input.NaamHuisarts, input.EmailHuisarts);
            var items = await _jeugdigeRepository.GetListAsync(input.FilterText, input.WoonsituatieMin, input.WoonsituatieMax, input.SamenstellingHuishoudenMin, input.SamenstellingHuishoudenMax, input.ToestemmingInformatiedeling, input.Notitie, input.Werkaantekening, input.InzageEigenDossier, input.GeheimDossier, input.NaamHuisarts, input.EmailHuisarts, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<JeugdigeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Jeugdige>, List<JeugdigeDto>>(items)
            };
        }

        public virtual async Task<JeugdigeDto> GetAsync(int id)
        {
            return ObjectMapper.Map<Jeugdige, JeugdigeDto>(await _jeugdigeRepository.GetAsync(id));
        }

        [Authorize(NEXTjeugdPermissions.Jeugdigen.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _jeugdigeRepository.DeleteAsync(id);
        }

        [Authorize(NEXTjeugdPermissions.Jeugdigen.Create)]
        public virtual async Task<JeugdigeDto> CreateAsync(JeugdigeCreateDto input)
        {

            var jeugdige = ObjectMapper.Map<JeugdigeCreateDto, Jeugdige>(input);
            jeugdige.TenantId = CurrentTenant.Id;
            jeugdige = await _jeugdigeRepository.InsertAsync(jeugdige, autoSave: true);
            return ObjectMapper.Map<Jeugdige, JeugdigeDto>(jeugdige);
        }

        [Authorize(NEXTjeugdPermissions.Jeugdigen.Edit)]
        public virtual async Task<JeugdigeDto> UpdateAsync(int id, JeugdigeUpdateDto input)
        {

            var jeugdige = await _jeugdigeRepository.GetAsync(id);
            ObjectMapper.Map(input, jeugdige);
            jeugdige = await _jeugdigeRepository.UpdateAsync(jeugdige, autoSave: true);
            return ObjectMapper.Map<Jeugdige, JeugdigeDto>(jeugdige);
        }
    }
}