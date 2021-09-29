using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NEXTjeugd.Clienten
{
    public interface IClientenAppService : IApplicationService
    {
        Task<PagedResultDto<ClientDto>> GetListAsync(GetClientenInput input);

        Task<ClientDto> GetAsync(int id);

        Task DeleteAsync(int id);

        Task<ClientDto> CreateAsync(ClientCreateDto input);

        Task<ClientDto> UpdateAsync(int id, ClientUpdateDto input);
    }
}