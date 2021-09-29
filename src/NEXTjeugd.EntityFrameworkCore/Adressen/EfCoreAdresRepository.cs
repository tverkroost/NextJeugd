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

namespace NEXTjeugd.Adressen
{
    public class EfCoreAdresRepository : EfCoreRepository<NEXTjeugdDbContext, Adres, Guid>, IAdresRepository
    {
        public EfCoreAdresRepository(IDbContextProvider<NEXTjeugdDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Adres>> GetListAsync(
            string filterText = null,
            string postcode = null,
            string straatnaam = null,
            string huisnummer = null,
            string woonplaats = null,
            string stadsdeel = null,
            DateTime? begindatumMin = null,
            DateTime? begindatumMax = null,
            string einddatum = null,
            bool? geheim = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, postcode, straatnaam, huisnummer, woonplaats, stadsdeel, begindatumMin, begindatumMax, einddatum, geheim);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AdresConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string postcode = null,
            string straatnaam = null,
            string huisnummer = null,
            string woonplaats = null,
            string stadsdeel = null,
            DateTime? begindatumMin = null,
            DateTime? begindatumMax = null,
            string einddatum = null,
            bool? geheim = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, postcode, straatnaam, huisnummer, woonplaats, stadsdeel, begindatumMin, begindatumMax, einddatum, geheim);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Adres> ApplyFilter(
            IQueryable<Adres> query,
            string filterText,
            string postcode = null,
            string straatnaam = null,
            string huisnummer = null,
            string woonplaats = null,
            string stadsdeel = null,
            DateTime? begindatumMin = null,
            DateTime? begindatumMax = null,
            string einddatum = null,
            bool? geheim = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Postcode.Contains(filterText) || e.Straatnaam.Contains(filterText) || e.Huisnummer.Contains(filterText) || e.Woonplaats.Contains(filterText) || e.Stadsdeel.Contains(filterText) || e.Einddatum.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(postcode), e => e.Postcode.Contains(postcode))
                    .WhereIf(!string.IsNullOrWhiteSpace(straatnaam), e => e.Straatnaam.Contains(straatnaam))
                    .WhereIf(!string.IsNullOrWhiteSpace(huisnummer), e => e.Huisnummer.Contains(huisnummer))
                    .WhereIf(!string.IsNullOrWhiteSpace(woonplaats), e => e.Woonplaats.Contains(woonplaats))
                    .WhereIf(!string.IsNullOrWhiteSpace(stadsdeel), e => e.Stadsdeel.Contains(stadsdeel))
                    .WhereIf(begindatumMin.HasValue, e => e.Begindatum >= begindatumMin.Value)
                    .WhereIf(begindatumMax.HasValue, e => e.Begindatum <= begindatumMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(einddatum), e => e.Einddatum.Contains(einddatum))
                    .WhereIf(geheim.HasValue, e => e.Geheim == geheim);
        }
    }
}