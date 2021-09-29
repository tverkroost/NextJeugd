using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NEXTjeugd.Adressen
{
    public interface IAdresRepository : IRepository<Adres, Guid>
    {
        Task<List<Adres>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}