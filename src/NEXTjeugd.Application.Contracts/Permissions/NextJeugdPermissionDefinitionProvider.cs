using NEXTjeugd.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace NEXTjeugd.Permissions
{
    public class NEXTjeugdPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(NEXTjeugdPermissions.GroupName);

            myGroup.AddPermission(NEXTjeugdPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
            myGroup.AddPermission(NEXTjeugdPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(NEXTjeugdPermissions.MyPermission1, L("Permission:MyPermission1"));

            var jeugdigePermission = myGroup.AddPermission(NEXTjeugdPermissions.Jeugdigen.Default, L("Permission:Jeugdigen"), MultiTenancySides.Tenant);
            jeugdigePermission.AddChild(NEXTjeugdPermissions.Jeugdigen.Create, L("Permission:Create"));
            jeugdigePermission.AddChild(NEXTjeugdPermissions.Jeugdigen.Edit, L("Permission:Edit"));
            jeugdigePermission.AddChild(NEXTjeugdPermissions.Jeugdigen.Delete, L("Permission:Delete"));

            var persoonPermission = myGroup.AddPermission(NEXTjeugdPermissions.Personen.Default, L("Permission:Personen"), MultiTenancySides.Tenant);
            persoonPermission.AddChild(NEXTjeugdPermissions.Personen.Create, L("Permission:Create"));
            persoonPermission.AddChild(NEXTjeugdPermissions.Personen.Edit, L("Permission:Edit"));
            persoonPermission.AddChild(NEXTjeugdPermissions.Personen.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NEXTjeugdResource>(name);
        }
    }
}