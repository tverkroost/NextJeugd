using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NEXTjeugd.Jeugdigen
{
    public interface IJeugdigeRepository : IRepository<Jeugdige, int>
    {
        Task<List<Jeugdige>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}