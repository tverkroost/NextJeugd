using NEXTjeugd.Personen;
using System;
using System.ComponentModel.DataAnnotations;

namespace NEXTjeugd.Jeugdigen
{
    public class JeugdigeUpdateDto: PersoonUpdateDto
    {
        public int Woonsituatie { get; set; }
        public int SamenstellingHuishouden { get; set; }
        public bool ToestemmingInformatiedeling { get; set; }
        public string Notitie { get; set; }
        public string Werkaantekening { get; set; }
        public bool InzageEigenDossier { get; set; }
        public bool GeheimDossier { get; set; }
        public string NaamHuisarts { get; set; }
        public string EmailHuisarts { get; set; }
    }
}