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

namespace NEXTjeugd.Jeugdigen
{
    public class EfCoreJeugdigeRepository : EfCoreRepository<NEXTjeugdDbContext, Jeugdige, int>, IJeugdigeRepository
    {
        public EfCoreJeugdigeRepository(IDbContextProvider<NEXTjeugdDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Jeugdige>> GetListAsync(
            string filterText = null,
            int? woonsituatieMin = null,
            int? woonsituatieMax = null,
            int? samenstellingHuishoudenMin = null,
            int? samenstellingHuishoudenMax = null,
            bool? toestemmingInformatiedeling = null,
            string notitie = null,
            string werkaantekening = null,
            bool? inzageEigenDossier = null,
            bool? geheimDossier = null,
            string naamHuisarts = null,
            string emailHuisarts = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, woonsituatieMin, woonsituatieMax, samenstellingHuishoudenMin, samenstellingHuishoudenMax, toestemmingInformatiedeling, notitie, werkaantekening, inzageEigenDossier, geheimDossier, naamHuisarts, emailHuisarts);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? JeugdigeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? woonsituatieMin = null,
            int? woonsituatieMax = null,
            int? samenstellingHuishoudenMin = null,
            int? samenstellingHuishoudenMax = null,
            bool? toestemmingInformatiedeling = null,
            string notitie = null,
            string werkaantekening = null,
            bool? inzageEigenDossier = null,
            bool? geheimDossier = null,
            string naamHuisarts = null,
            string emailHuisarts = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, woonsituatieMin, woonsituatieMax, samenstellingHuishoudenMin, samenstellingHuishoudenMax, toestemmingInformatiedeling, notitie, werkaantekening, inzageEigenDossier, geheimDossier, naamHuisarts, emailHuisarts);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Jeugdige> ApplyFilter(
            IQueryable<Jeugdige> query,
            string filterText,
            int? woonsituatieMin = null,
            int? woonsituatieMax = null,
            int? samenstellingHuishoudenMin = null,
            int? samenstellingHuishoudenMax = null,
            bool? toestemmingInformatiedeling = null,
            string notitie = null,
            string werkaantekening = null,
            bool? inzageEigenDossier = null,
            bool? geheimDossier = null,
            string naamHuisarts = null,
            string emailHuisarts = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Notitie.Contains(filterText) || e.Werkaantekening.Contains(filterText) || e.NaamHuisarts.Contains(filterText) || e.EmailHuisarts.Contains(filterText))
                    .WhereIf(woonsituatieMin.HasValue, e => e.Woonsituatie >= woonsituatieMin.Value)
                    .WhereIf(woonsituatieMax.HasValue, e => e.Woonsituatie <= woonsituatieMax.Value)
                    .WhereIf(samenstellingHuishoudenMin.HasValue, e => e.SamenstellingHuishouden >= samenstellingHuishoudenMin.Value)
                    .WhereIf(samenstellingHuishoudenMax.HasValue, e => e.SamenstellingHuishouden <= samenstellingHuishoudenMax.Value)
                    .WhereIf(toestemmingInformatiedeling.HasValue, e => e.ToestemmingInformatiedeling == toestemmingInformatiedeling)
                    .WhereIf(!string.IsNullOrWhiteSpace(notitie), e => e.Notitie.Contains(notitie))
                    .WhereIf(!string.IsNullOrWhiteSpace(werkaantekening), e => e.Werkaantekening.Contains(werkaantekening))
                    .WhereIf(inzageEigenDossier.HasValue, e => e.InzageEigenDossier == inzageEigenDossier)
                    .WhereIf(geheimDossier.HasValue, e => e.GeheimDossier == geheimDossier)
                    .WhereIf(!string.IsNullOrWhiteSpace(naamHuisarts), e => e.NaamHuisarts.Contains(naamHuisarts))
                    .WhereIf(!string.IsNullOrWhiteSpace(emailHuisarts), e => e.EmailHuisarts.Contains(emailHuisarts));
        }
    }
}