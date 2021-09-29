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
using NEXTjeugd.Clienten;

namespace NEXTjeugd.Clienten
{

    [Authorize(NEXTjeugdPermissions.Clienten.Default)]
    public class ClientenAppService : ApplicationService, IClientenAppService
    {
        private readonly IClientRepository _clientRepository;

        public ClientenAppService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public virtual async Task<PagedResultDto<ClientDto>> GetListAsync(GetClientenInput input)
        {
            var totalCount = await _clientRepository.GetCountAsync(input.FilterText, input.Naam);
            var items = await _clientRepository.GetListAsync(input.FilterText, input.Naam, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ClientDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Client>, List<ClientDto>>(items)
            };
        }

        public virtual async Task<ClientDto> GetAsync(int id)
        {
            return ObjectMapper.Map<Client, ClientDto>(await _clientRepository.GetAsync(id));
        }

        [Authorize(NEXTjeugdPermissions.Clienten.Delete)]
        public virtual async Task DeleteAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        [Authorize(NEXTjeugdPermissions.Clienten.Create)]
        public virtual async Task<ClientDto> CreateAsync(ClientCreateDto input)
        {

            var client = ObjectMapper.Map<ClientCreateDto, Client>(input);
            client.TenantId = CurrentTenant.Id;
            client = await _clientRepository.InsertAsync(client, autoSave: true);
            return ObjectMapper.Map<Client, ClientDto>(client);
        }

        [Authorize(NEXTjeugdPermissions.Clienten.Edit)]
        public virtual async Task<ClientDto> UpdateAsync(int id, ClientUpdateDto input)
        {

            var client = await _clientRepository.GetAsync(id);
            ObjectMapper.Map(input, client);
            client = await _clientRepository.UpdateAsync(client, autoSave: true);
            return ObjectMapper.Map<Client, ClientDto>(client);
        }
    }
}