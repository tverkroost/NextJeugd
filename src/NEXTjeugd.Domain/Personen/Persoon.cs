using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace NEXTjeugd.Personen
{
    public abstract class Persoon : FullAuditedAggregateRoot<int>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Roepnaam { get; set; }

        [CanBeNull]
        public virtual string Voorletters { get; set; }

        [CanBeNull]
        public virtual string Tussenvoegsel { get; set; }

        [CanBeNull]
        public virtual string Achternaam { get; set; }

        [CanBeNull]
        public virtual string BSN { get; set; }

        [CanBeNull]
        public virtual string Geslacht { get; set; }

        public virtual DateTime Geboortedatum { get; set; }

        [CanBeNull]
        public virtual string Geboorteland { get; set; }

        public Persoon()
        {

        }

        public Persoon(int id, string roepnaam, string voorletters, string tussenvoegsel, string achternaam, string bSN, DateTime geboortedatum, string geboorteland, string geslacht = null)
        {
            Id = id;
            Check.NotNull(roepnaam, nameof(roepnaam));
            Roepnaam = roepnaam;
            Voorletters = voorletters;
            Tussenvoegsel = tussenvoegsel;
            Achternaam = achternaam;
            BSN = bSN;
            Geboortedatum = geboortedatum;
            Geboorteland = geboorteland;
            Geslacht = geslacht;
        }
    }
}