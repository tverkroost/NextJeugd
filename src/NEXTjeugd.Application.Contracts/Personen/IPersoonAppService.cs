using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NEXTjeugd.Personen
{
    public interface IPersonenAppService : IApplicationService
    {
        Task<PagedResultDto<PersoonDto>> GetListAsync(GetPersonenInput input);

        Task<PersoonDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<PersoonDto> CreateAsync(PersoonCreateDto input);

        Task<PersoonDto> UpdateAsync(int id, PersoonUpdateDto input);
    }
}