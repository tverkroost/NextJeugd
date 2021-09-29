namespace NEXTjeugd.Jeugdigen
{
    public static class JeugdigeConsts
    {
        private const string DefaultSorting = "{0}Woonsituatie asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Jeugdige." : string.Empty);
        }

    }
}