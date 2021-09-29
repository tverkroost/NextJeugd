namespace NEXTjeugd.Personen
{
    public static class PersoonConsts
    {
        private const string DefaultSorting = "{0}Roepnaam asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Persoon." : string.Empty);
        }

    }
}