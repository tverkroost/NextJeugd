using NEXTjeugd.Adressen;
using NEXTjeugd.Personen;
using AutoMapper;
using NEXTjeugd.Jeugdigen;

namespace NEXTjeugd.Blazor
{
    public class NEXTjeugdBlazorAutoMapperProfile : Profile
    {
        public NEXTjeugdBlazorAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Blazor project.

            CreateMap<JeugdigeDto, JeugdigeUpdateDto>();

            CreateMap<PersoonDto, PersoonUpdateDto>();

            CreateMap<AdresDto, AdresUpdateDto>();
        }
    }
}