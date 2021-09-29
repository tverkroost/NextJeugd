using System;
using System.ComponentModel.DataAnnotations;

namespace NEXTjeugd.Adressen
{
    public class AdresCreateDto
    {
        public string Postcode { get; set; }
        public string Straatnaam { get; set; }
        public string Huisnummer { get; set; }
        public string Woonplaats { get; set; }
        public string Stadsdeel { get; set; }
        public DateTime Begindatum { get; set; }
        public string Einddatum { get; set; }
        public bool Geheim { get; set; }
    }
}