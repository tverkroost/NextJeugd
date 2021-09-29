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

        public class Personen
        {
            public const string Default = GroupName + ".Personen";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Jeugdigen
        {
            public const string Default = GroupName + ".Jeugdigen";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}