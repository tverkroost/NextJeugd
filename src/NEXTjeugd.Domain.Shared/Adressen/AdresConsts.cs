namespace NEXTjeugd.Adressen
{
    public static class AdresConsts
    {
        private const string DefaultSorting = "{0}Postcode asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Adres." : string.Empty);
        }

    }
}