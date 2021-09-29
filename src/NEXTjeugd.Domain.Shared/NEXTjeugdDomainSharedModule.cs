using NEXTjeugd.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Abp.VirtualFileSystem;
using Volo.Saas;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Commercial.SuiteTemplates;
using Volo.Abp.GlobalFeatures;

namespace NEXTjeugd
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpIdentityProDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(LanguageManagementDomainSharedModule),
        typeof(SaasDomainSharedModule),
        typeof(TextTemplateManagementDomainSharedModule),
        typeof(VoloAbpCommercialSuiteTemplatesModule),
        typeof(LeptonThemeManagementDomainSharedModule),
        typeof(AbpGlobalFeaturesModule),
        typeof(BlobStoringDatabaseDomainSharedModule)
        )]
    public class NEXTjeugdDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            NEXTjeugdGlobalFeatureConfigurator.Configure();
            NEXTjeugdModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NEXTjeugdDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NEXTjeugdResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/NEXTjeugd");

                options.DefaultResourceType = typeof(NEXTjeugdResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("NEXTjeugd", typeof(NEXTjeugdResource));
            });
        }
    }
}
