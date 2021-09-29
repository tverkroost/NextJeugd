using Volo.Abp.Application.Dtos;
using System;

namespace NEXTjeugd.Jeugdigen
{
    public class GetJeugdigenInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? WoonsituatieMin { get; set; }
        public int? WoonsituatieMax { get; set; }
        public int? SamenstellingHuishoudenMin { get; set; }
        public int? SamenstellingHuishoudenMax { get; set; }
        public bool? ToestemmingInformatiedeling { get; set; }
        public string Notitie { get; set; }
        public string Werkaantekening { get; set; }
        public bool? InzageEigenDossier { get; set; }
        public bool? GeheimDossier { get; set; }
        public string NaamHuisarts { get; set; }
        public string EmailHuisarts { get; set; }

        public GetJeugdigenInput()
        {

        }
    }
}