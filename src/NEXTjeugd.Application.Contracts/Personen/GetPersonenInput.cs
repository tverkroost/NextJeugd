using Volo.Abp.Application.Dtos;
using System;

namespace NEXTjeugd.Personen
{
    public class GetPersonenInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Roepnaam { get; set; }
        public string Voorletters { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string BSN { get; set; }
        public string Geslacht { get; set; }
        public DateTime? GeboortedatumMin { get; set; }
        public DateTime? GeboortedatumMax { get; set; }
        public string Geboorteland { get; set; }

        public GetPersonenInput()
        {

        }
    }
}