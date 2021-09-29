using System;
using System.ComponentModel.DataAnnotations;

namespace NEXTjeugd.Personen
{
    public class PersoonCreateDto
    {
        [Required]
        public string Roepnaam { get; set; }
        public string Voorletters { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string BSN { get; set; }
        public string Geslacht { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Geboorteland { get; set; }
    }
}