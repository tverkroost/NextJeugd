using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NEXTjeugd.Clienten
{
    public interface IClientRepository : IRepository<Client, int>
    {
        Task<List<Client>> GetListAsync(
            string filterText = null,
            string naam = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string naam = null,
            CancellationToken cancellationToken = default);
    }
}