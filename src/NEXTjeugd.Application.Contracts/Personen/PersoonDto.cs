using System;
using Volo.Abp.Application.Dtos;

namespace NEXTjeugd.Personen
{
    public class PersoonDto : FullAuditedEntityDto<int>
    {
        public string Roepnaam { get; set; }
        public string Voorletters { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string BSN { get; set; }
        public string Geslacht { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Geboorteland { get; set; }
        public string Type { get; set; }
    }
}