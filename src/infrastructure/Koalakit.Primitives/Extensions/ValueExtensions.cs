namespace Kamel.Primitives.Extensions;

public static class ValueExtensions
{
    public static bool AnyOf<T>(this T value, params T[] values)
    {
        if (values == null || values.Length == 0)
            return false;

        return values.Contains(value);
    }

    public static bool AnyOf<T>(this T value, IEnumerable<T> values)
    {
        if (values == null)
            return false;

        return values.Contains(value);
    }

    public static bool AnyOf<T>(this T value, IEqualityComparer<T> comparer, params T[] values)
    {
        if (values == null || values.Length == 0)
            return false;

        return values.Contains(value, comparer);
    }

    public static bool AnyOf<T>(this T value, IEnumerable<T> values, IEqualityComparer<T> comparer)
    {
        if (values == null)
            return false;

        return values.Contains(value, comparer);
    }
}