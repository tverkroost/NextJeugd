using Volo.Abp.Application.Dtos;
using System;

namespace NEXTjeugd.Adressen
{
    public class GetAdressenInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Postcode { get; set; }
        public string Straatnaam { get; set; }
        public string Huisnummer { get; set; }
        public string Woonplaats { get; set; }
        public string Stadsdeel { get; set; }
        public DateTime? BegindatumMin { get; set; }
        public DateTime? BegindatumMax { get; set; }
        public string Einddatum { get; set; }
        public bool? Geheim { get; set; }

        public GetAdressenInput()
        {

        }
    }
}