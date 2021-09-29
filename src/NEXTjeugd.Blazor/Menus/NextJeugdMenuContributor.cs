using NEXTjeugd.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NEXTjeugd.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.AuditLogging.Blazor.Menus;
using Volo.Abp.Identity.Pro.Blazor.Navigation;
using Volo.Abp.IdentityServer.Blazor.Navigation;
using Volo.Abp.LanguageManagement.Blazor.Menus;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TextTemplateManagement.Blazor.Menus;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using Volo.Saas.Host.Blazor.Navigation;

namespace NEXTjeugd.Blazor.Menus
{
    public class NEXTjeugdMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<NEXTjeugdResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    NEXTjeugdMenus.Home,
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home",
                    order: 1
                )
            );

            /* Example nested menu definition:

            context.Menu.AddItem(
                new ApplicationMenuItem("Menu0", "Menu Level 0")
                .AddItem(new ApplicationMenuItem("Menu0.1", "Menu Level 0.1", url: "/test01"))
                .AddItem(
                    new ApplicationMenuItem("Menu0.2", "Menu Level 0.2")
                        .AddItem(new ApplicationMenuItem("Menu0.2.1", "Menu Level 0.2.1", url: "/test021"))
                        .AddItem(new ApplicationMenuItem("Menu0.2.2", "Menu Level 0.2.2")
                            .AddItem(new ApplicationMenuItem("Menu0.2.2.1", "Menu Level 0.2.2.1", "/test0221"))
                            .AddItem(new ApplicationMenuItem("Menu0.2.2.2", "Menu Level 0.2.2.2", "/test0222"))
                        )
                        .AddItem(new ApplicationMenuItem("Menu0.2.3", "Menu Level 0.2.3", url: "/test023"))
                        .AddItem(new ApplicationMenuItem("Menu0.2.4", "Menu Level 0.2.4", url: "/test024")
                            .AddItem(new ApplicationMenuItem("Menu0.2.4.1", "Menu Level 0.2.4.1", "/test0241"))
                    )
                    .AddItem(new ApplicationMenuItem("Menu0.2.5", "Menu Level 0.2.5", url: "/test025"))
                )
                .AddItem(new ApplicationMenuItem("Menu0.2", "Menu Level 0.2", url: "/test02"))
            );

            */

            context.Menu.SetSubItemOrder(SaasHostMenus.GroupName, 2);

            //Administration
            var administration = context.Menu.GetAdministration();
            administration.Order = 4;

            //Administration->Identity
            administration.SetSubItemOrder(IdentityProMenus.GroupName, 1);

            //Administration->Identity Server
            administration.SetSubItemOrder(AbpIdentityServerMenuNames.GroupName, 2);

            //Administration->Language Management
            administration.SetSubItemOrder(LanguageManagementMenus.GroupName, 3);

            //Administration->Text Template Management
            administration.SetSubItemOrder(TextTemplateManagementMenus.GroupName, 4);

            //Administration->Audit Logs
            administration.SetSubItemOrder(AbpAuditLoggingMenus.GroupName, 5);

            //Administration->Settings
            administration.SetSubItemOrder(SettingManagementMenus.GroupName, 6);

            context.Menu.AddItem(
                new ApplicationMenuItem(
                    NEXTjeugdMenus.Clienten,
                    l["Menu:Clienten"],
                    url: "/clienten",
                    icon: "fa fa-file-alt",
                    requiredPermissionName: NEXTjeugdPermissions.Clienten.Default)
            );
            return Task.CompletedTask;
        }
    }
}