using NEXTjeugd.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace NEXTjeugd.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(NEXTjeugdEntityFrameworkCoreModule),
        typeof(NEXTjeugdApplicationContractsModule)
    )]
    public class NEXTjeugdDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });
        }
    }
}
