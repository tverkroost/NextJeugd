using System;
using NEXTjeugd.Shared;
using Volo.Abp.AutoMapper;
using NEXTjeugd.Clienten;
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

            CreateMap<ClientCreateDto, Client>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<ClientUpdateDto, Client>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id).Ignore(x => x.TenantId);
            CreateMap<Client, ClientDto>();
        }
    }
}