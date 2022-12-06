using System.Text.RegularExpressions;

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
        public static bool AnyEmptyorNull(params string[] values)
        {
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value)) return true;
            }
            return false;
        }
        public static bool IsValidOrEmptyEmail(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return true;
            return Regex.Match(value, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").Success;
        }

        public static bool IsValidEmail(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.Match(value, "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").Success;
        }
    }
}
