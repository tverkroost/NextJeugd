using System;
using Volo.Abp.Application.Dtos;

namespace NEXTjeugd.Clienten
{
    public class ClientDto : FullAuditedEntityDto<int>
    {
        public string Naam { get; set; }
    }
}