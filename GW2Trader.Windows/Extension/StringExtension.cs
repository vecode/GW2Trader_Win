namespace GW2Trader.Desktop.Extension
{
    public static class StringExtension
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            if (value == null)
            {
                return true;
            }

            value = value.Trim();
            return string.IsNullOrEmpty(value);
        }
    }
}
