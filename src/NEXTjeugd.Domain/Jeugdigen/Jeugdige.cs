using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;
using NEXTjeugd.Personen;

namespace NEXTjeugd.Jeugdigen
{
    public class Jeugdige :Persoon
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int Woonsituatie { get; set; }

        public virtual int SamenstellingHuishouden { get; set; }

        public virtual bool ToestemmingInformatiedeling { get; set; }

        [CanBeNull]
        public virtual string Notitie { get; set; }

        [CanBeNull]
        public virtual string Werkaantekening { get; set; }

        public virtual bool InzageEigenDossier { get; set; }

        public virtual bool GeheimDossier { get; set; }

        [CanBeNull]
        public virtual string NaamHuisarts { get; set; }

        [CanBeNull]
        public virtual string EmailHuisarts { get; set; }

        public Jeugdige()
        {

        }

        public Jeugdige(int id, int woonsituatie, int samenstellingHuishouden, bool toestemmingInformatiedeling, string notitie, string werkaantekening, bool inzageEigenDossier, bool geheimDossier, string naamHuisarts, string emailHuisarts)
        {
            Id = id;
            Woonsituatie = woonsituatie;
            SamenstellingHuishouden = samenstellingHuishouden;
            ToestemmingInformatiedeling = toestemmingInformatiedeling;
            Notitie = notitie;
            Werkaantekening = werkaantekening;
            InzageEigenDossier = inzageEigenDossier;
            GeheimDossier = geheimDossier;
            NaamHuisarts = naamHuisarts;
            EmailHuisarts = emailHuisarts;
        }
    }
}