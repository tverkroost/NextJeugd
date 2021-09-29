using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace NEXTjeugd.Adressen
{
    public class Adres : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string Postcode { get; set; }

        [CanBeNull]
        public virtual string Straatnaam { get; set; }

        [CanBeNull]
        public virtual string Huisnummer { get; set; }

        [CanBeNull]
        public virtual string Woonplaats { get; set; }

        [CanBeNull]
        public virtual string Stadsdeel { get; set; }

        public virtual DateTime Begindatum { get; set; }

        [CanBeNull]
        public virtual string Einddatum { get; set; }

        public virtual bool Geheim { get; set; }

        public Adres()
        {

        }

        public Adres(Guid id, string postcode, string straatnaam, string huisnummer, string woonplaats, string stadsdeel, DateTime begindatum, string einddatum, bool geheim)
        {
            Id = id;
            Postcode = postcode;
            Straatnaam = straatnaam;
            Huisnummer = huisnummer;
            Woonplaats = woonplaats;
            Stadsdeel = stadsdeel;
            Begindatum = begindatum;
            Einddatum = einddatum;
            Geheim = geheim;
        }
    }
}