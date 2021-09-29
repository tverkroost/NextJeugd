using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using NEXTjeugd.EntityFrameworkCore;

namespace NEXTjeugd.Clienten
{
    public class EfCoreClientRepository : EfCoreRepository<NEXTjeugdDbContext, Client, int>, IClientRepository
    {
        public EfCoreClientRepository(IDbContextProvider<NEXTjeugdDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Client>> GetListAsync(
            string filterText = null,
            string naam = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, naam);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ClientConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string naam = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, naam);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Client> ApplyFilter(
            IQueryable<Client> query,
            string filterText,
            string naam = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Naam.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(naam), e => e.Naam.Contains(naam));
        }
    }
}