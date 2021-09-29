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

            var clientPermission = myGroup.AddPermission(NEXTjeugdPermissions.Clienten.Default, L("Permission:Clienten"), MultiTenancySides.Tenant);
            clientPermission.AddChild(NEXTjeugdPermissions.Clienten.Create, L("Permission:Create"));
            clientPermission.AddChild(NEXTjeugdPermissions.Clienten.Edit, L("Permission:Edit"));
            clientPermission.AddChild(NEXTjeugdPermissions.Clienten.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NEXTjeugdResource>(name);
        }
    }
}