using NEXTjeugd.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace NEXTjeugd
{
    [DependsOn(
        typeof(NEXTjeugdEntityFrameworkCoreTestModule)
        )]
    public class NEXTjeugdDomainTestModule : AbpModule
    {

    }
}