namespace NEXTjeugd.Clienten
{
    public static class ClientConsts
    {
        private const string DefaultSorting = "{0}Naam asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Client." : string.Empty);
        }

    }
}