using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NEXTjeugd.Personen
{
    public interface IPersoonRepository : IRepository<Persoon, int>
    {
        Task<List<Persoon>> GetListAsync(
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
            string type = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            string type = null,
            CancellationToken cancellationToken = default);
    }
}