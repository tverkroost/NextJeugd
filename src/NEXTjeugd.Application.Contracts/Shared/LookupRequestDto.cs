using Volo.Abp.Application.Dtos;

namespace NEXTjeugd.Shared
{
    public class LookupRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }

        public LookupRequestDto()
        {
            MaxResultCount = MaxMaxResultCount;
        }
    }
}