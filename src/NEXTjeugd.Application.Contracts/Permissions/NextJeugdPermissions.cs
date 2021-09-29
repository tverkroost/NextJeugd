namespace NEXTjeugd.Permissions
{
    public static class NEXTjeugdPermissions
    {
        public const string GroupName = "NEXTjeugd";

        public static class Dashboard
        {
            public const string DashboardGroup = GroupName + ".Dashboard";
            public const string Host = DashboardGroup + ".Host";
            public const string Tenant = DashboardGroup + ".Tenant";
        }

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
    }
}
