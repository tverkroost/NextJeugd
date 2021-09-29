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

namespace NEXTjeugd.Personen
{
    public class EfCorePersoonRepository : EfCoreRepository<NEXTjeugdDbContext, Persoon, int>, IPersoonRepository
    {
        public EfCorePersoonRepository(IDbContextProvider<NEXTjeugdDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Persoon>> GetListAsync(
            string filterText = null,
            string roepnaam = null,
            string voorletters = null,
            string tussenvoegsel = null,
            string achternaam = null,
            string bSN = null,
            string geslacht = null,
            DateTime? geboortedatumMin = null,
            DateTime? geboortedatumMax = null,
            string geboorteland = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, roepnaam, voorletters, tussenvoegsel, achternaam, bSN, geslacht, geboortedatumMin, geboortedatumMax, geboorteland);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PersoonConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string roepnaam = null,
            string voorletters = null,
            string tussenvoegsel = null,
            string achternaam = null,
            string bSN = null,
            string geslacht = null,
            DateTime? geboortedatumMin = null,
            DateTime? geboortedatumMax = null,
            string geboorteland = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, roepnaam, voorletters, tussenvoegsel, achternaam, bSN, geslacht, geboortedatumMin, geboortedatumMax, geboorteland);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Persoon> ApplyFilter(
            IQueryable<Persoon> query,
            string filterText,
            string roepnaam = null,
            string voorletters = null,
            string tussenvoegsel = null,
            string achternaam = null,
            string bSN = null,
            string geslacht = null,
            DateTime? geboortedatumMin = null,
            DateTime? geboortedatumMax = null,
            string geboorteland = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Roepnaam.Contains(filterText) || e.Voorletters.Contains(filterText) || e.Tussenvoegsel.Contains(filterText) || e.Achternaam.Contains(filterText) || e.BSN.Contains(filterText) || e.Geslacht.Contains(filterText) || e.Geboorteland.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(roepnaam), e => e.Roepnaam.Contains(roepnaam))
                    .WhereIf(!string.IsNullOrWhiteSpace(voorletters), e => e.Voorletters.Contains(voorletters))
                    .WhereIf(!string.IsNullOrWhiteSpace(tussenvoegsel), e => e.Tussenvoegsel.Contains(tussenvoegsel))
                    .WhereIf(!string.IsNullOrWhiteSpace(achternaam), e => e.Achternaam.Contains(achternaam))
                    .WhereIf(!string.IsNullOrWhiteSpace(bSN), e => e.BSN.Contains(bSN))
                    .WhereIf(!string.IsNullOrWhiteSpace(geslacht), e => e.Geslacht.Contains(geslacht))
                    .WhereIf(geboortedatumMin.HasValue, e => e.Geboortedatum >= geboortedatumMin.Value)
                    .WhereIf(geboortedatumMax.HasValue, e => e.Geboortedatum <= geboortedatumMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(geboorteland), e => e.Geboorteland.Contains(geboorteland));
        }
    }
}