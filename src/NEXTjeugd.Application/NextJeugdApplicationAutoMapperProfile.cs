using NEXTjeugd.Adressen;
using NEXTjeugd.Jeugdigen;
using NEXTjeugd.Personen;
using Volo.Abp.AutoMapper;
using AutoMapper;

namespace NEXTjeugd
{
    public class NEXTjeugdApplicationAutoMapperProfile : Profile
    {
        public NEXTjeugdApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<PersoonCreateDto, Persoon>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<PersoonUpdateDto, Persoon>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<Persoon, PersoonDto>();

            CreateMap<JeugdigeCreateDto, Jeugdige>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<JeugdigeUpdateDto, Jeugdige>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<Jeugdige, JeugdigeDto>();

            CreateMap<AdresCreateDto, Adres>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<AdresUpdateDto, Adres>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<Adres, AdresDto>();
        }
    }
}