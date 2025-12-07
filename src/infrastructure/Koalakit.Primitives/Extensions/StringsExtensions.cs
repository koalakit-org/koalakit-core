using System.Security.Cryptography;
using System.Text;

namespace Kamel.Primitives.Extensions;

public static partial class StringsExtensions
{
    public static byte[] ComputeSha256Hash(this string input)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        return SHA256.HashData(bytes);
    }

    public static string AsBase64Hash(this string input)
    {
        var hashBytes = input.ComputeSha256Hash();
        return Convert.ToBase64String(hashBytes).Replace("/", "_").Replace("+", "-").Replace("=", "");
    }

    public static string? ToSnakeCase(this string? str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return str;

        var sb = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];

            if (c == '_')
            {
                sb.Append(c);
                continue;
            }

            if (char.IsUpper(c))
            {
                if (i > 0)
                    sb.Append('_');

                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    public static bool IsValidEmail(this string email)
    {
        return MyRegex().IsMatch(email);
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}