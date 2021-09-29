using System;
using System.IO;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NEXTjeugd.Blazor.Menus;
using NEXTjeugd.EntityFrameworkCore;
using NEXTjeugd.Localization;
using NEXTjeugd.MultiTenancy;
using Microsoft.OpenApi.Models;
using NEXTjeugd.Blazor.Components.Layout;
using Volo.Abp;
using Volo.Abp.Account.Pro.Admin.Blazor.Server;
using Volo.Abp.Account.Public.Web.ExternalProviders;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Components.Server.LeptonTheme;
using Volo.Abp.AspNetCore.Components.Server.LeptonTheme.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.Blazor.Server;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Pro.Blazor.Server;
using Volo.Abp.IdentityServer.Blazor.Server;
using Volo.Abp.LanguageManagement.Blazor.Server;
using Volo.Abp.LeptonTheme.Management.Blazor.Server;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TextTemplateManagement.Blazor.Server;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.Saas.Host.Blazor.Server;
using Volo.Abp.MultiTenancy;

namespace NEXTjeugd.Blazor
{
    [DependsOn(
        typeof(NEXTjeugdApplicationModule),
        typeof(NEXTjeugdEntityFrameworkCoreModule),
        typeof(NEXTjeugdHttpApiModule),
        typeof(AbpAspNetCoreMvcUiLeptonThemeModule),
        typeof(AbpAutofacModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAspNetCoreComponentsServerLeptonThemeModule),
        typeof(AbpAccountPublicWebIdentityServerModule),
        typeof(AbpAccountAdminBlazorServerModule),
        typeof(AbpAuditLoggingBlazorServerModule),
        typeof(AbpIdentityProBlazorServerModule),
        typeof(LeptonThemeManagementBlazorServerModule),
        typeof(AbpIdentityServerBlazorServerModule),
        typeof(LanguageManagementBlazorServerModule),
        typeof(SaasHostBlazorServerModule),
        typeof(TextTemplateManagementBlazorServerModule)
       )]
    public class NEXTjeugdBlazorModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(NEXTjeugdResource),
                    typeof(NEXTjeugdDomainModule).Assembly,
                    typeof(NEXTjeugdDomainSharedModule).Assembly,
                    typeof(NEXTjeugdApplicationModule).Assembly,
                    typeof(NEXTjeugdApplicationContractsModule).Assembly,
                    typeof(NEXTjeugdBlazorModule).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpTenantResolveOptions>(options =>
            {
                options.AddDomainTenantResolver("{0}.nextjeugd.nl");
            });

            ConfigureUrls(configuration);
            ConfigureBundles();
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureSwaggerServices(context.Services);
            ConfigureExternalProviders(context);
            ConfigureAutoApiControllers();
            ConfigureBlazorise(context);
            ConfigureRouter(context);
            ConfigureMenu(context);
            ConfigureLeptonTheme();
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureBundles()
        {
            Configure<AbpBundlingOptions>(options =>
            {
                // MVC UI
                options.StyleBundles.Configure(
                    LeptonThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/global-styles.css");
                    }
                );

                // Blazor UI
                options.StyleBundles.Configure(
                    BlazorLeptonThemeBundles.Styles.Global,
                    bundle =>
                    {
                        bundle.AddFiles("/blazor-global-styles.css");
                        //You can remove the following line if you don't use Blazor CSS isolation for components
                        bundle.AddFiles("/NEXTjeugd.Blazor.styles.css");
                    }
                );
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "NEXTjeugd";
                });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<NEXTjeugdDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}NEXTjeugd.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<NEXTjeugdDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}NEXTjeugd.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<NEXTjeugdApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}NEXTjeugd.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<NEXTjeugdApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}NEXTjeugd.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<NEXTjeugdBlazorModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddAbpSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NEXTjeugd API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }

        private void ConfigureExternalProviders(ServiceConfigurationContext context)
        {
            context.Services.AddAuthentication()
                .AddGoogle(GoogleDefaults.AuthenticationScheme, _ => {})
                .WithDynamicOptions<GoogleOptions, GoogleHandler>(
                    GoogleDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.WithProperty(x => x.ClientId);
                        options.WithProperty(x => x.ClientSecret, isSecret: true);
                    }
                )
                .AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
                {
                    //Personal Microsoft accounts as an example.
                    options.AuthorizationEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize";
                    options.TokenEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
                })
                .WithDynamicOptions<MicrosoftAccountOptions, MicrosoftAccountHandler>(
                    MicrosoftAccountDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.WithProperty(x => x.ClientId);
                        options.WithProperty(x => x.ClientSecret, isSecret: true);
                    }
                )
                .AddTwitter(TwitterDefaults.AuthenticationScheme, options => options.RetrieveUserDetails = true)
                .WithDynamicOptions<TwitterOptions, TwitterHandler>(
                    TwitterDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.WithProperty(x => x.ConsumerKey);
                        options.WithProperty(x => x.ConsumerSecret, isSecret: true);
                    }
                );
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("ar", "ar", "العربية", "ae"));
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
                options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
                options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish", "fi"));
                options.Languages.Add(new LanguageInfo("fr", "fr", "Français", "fr"));
                options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
                options.Languages.Add(new LanguageInfo("it", "it", "Italian", "it"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak", "sk"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
                options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
                options.Languages.Add(new LanguageInfo("es", "es", "Español"));
            });
        }


        private void ConfigureBlazorise(ServiceConfigurationContext context)
        {
            context.Services
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
        }

        private void ConfigureMenu(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new NEXTjeugdMenuContributor());
            });
        }
        
        private void ConfigureLeptonTheme()
        {
            Configure<Volo.Abp.AspNetCore.Components.Web.LeptonTheme.LeptonThemeOptions>(options =>
            {
                options.FooterComponent = typeof(MainFooterComponent);
            });
        }

        private void ConfigureRouter(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(NEXTjeugdBlazorModule).Assembly;
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(NEXTjeugdApplicationModule).Assembly);
            });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<NEXTjeugdBlazorModule>();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseUnitOfWork();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "NEXTjeugd API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
