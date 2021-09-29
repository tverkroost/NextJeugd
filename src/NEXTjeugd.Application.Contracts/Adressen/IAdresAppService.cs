using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NEXTjeugd.Adressen
{
    public interface IAdressenAppService : IApplicationService
    {
        Task<PagedResultDto<AdresDto>> GetListAsync(GetAdressenInput input);

        Task<AdresDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<AdresDto> CreateAsync(AdresCreateDto input);

        Task<AdresDto> UpdateAsync(Guid id, AdresUpdateDto input);
    }
}