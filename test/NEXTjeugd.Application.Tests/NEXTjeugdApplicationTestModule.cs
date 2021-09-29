using Volo.Abp.Modularity;

namespace NEXTjeugd
{
    [DependsOn(
        typeof(NEXTjeugdApplicationModule),
        typeof(NEXTjeugdDomainTestModule)
        )]
    public class NEXTjeugdApplicationTestModule : AbpModule
    {

    }
}