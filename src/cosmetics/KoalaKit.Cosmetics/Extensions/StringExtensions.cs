namespace System
{
    public static class StringExtensions
    {
        public static string NormalizeValue(this string? value)
        {
            var result = value.GetValueOrDefault();
            return result.Trim().ToLower();
        }

        public static string GetValueOrDefault(this string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            return value;
        }
    }
}
