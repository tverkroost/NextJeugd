using NEXTjeugd.Clienten;
using AutoMapper;

namespace NEXTjeugd.Blazor
{
    public class NEXTjeugdBlazorAutoMapperProfile : Profile
    {
        public NEXTjeugdBlazorAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Blazor project.

            CreateMap<ClientDto, ClientUpdateDto>();
        }
    }
}