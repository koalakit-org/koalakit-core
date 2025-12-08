namespace Koalakit.Primitives;

public static class AppClaimNames
{
    public const string Scope = "scope";

    public static IEnumerable<string> ListAllClaims()
    {
        return typeof(AppClaimNames)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy)
            .Where(field => field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string))
            .Select(field => field.GetValue(null) as string ?? string.Empty)
            .Where(claim => !string.IsNullOrEmpty(claim));
    }

    public static bool IsValidClaim(string? claim)
    {
        if (string.IsNullOrEmpty(claim))
        {
            return false;
        }
        return ListAllClaims().Contains(claim, StringComparer.OrdinalIgnoreCase);
    }
}