using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NEXTjeugd.Jeugdigen
{
    public interface IJeugdigenAppService : IApplicationService
    {
        Task<PagedResultDto<JeugdigeDto>> GetListAsync(GetJeugdigenInput input);

        Task<JeugdigeDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<JeugdigeDto> CreateAsync(JeugdigeCreateDto input);

        Task<JeugdigeDto> UpdateAsync(int id, JeugdigeUpdateDto input);
    }
}