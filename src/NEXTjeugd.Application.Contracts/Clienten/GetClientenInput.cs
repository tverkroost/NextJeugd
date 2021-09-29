using Volo.Abp.Application.Dtos;
using System;

namespace NEXTjeugd.Clienten
{
    public class GetClientenInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Naam { get; set; }

        public GetClientenInput()
        {

        }
    }
}