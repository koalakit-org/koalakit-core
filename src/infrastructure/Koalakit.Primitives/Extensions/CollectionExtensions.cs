using System.Diagnostics.CodeAnalysis;

namespace Koalakit.Primitives.Extensions;

public static class CollectionExtensions
{
    public static bool HasItems<T>([NotNullWhen(true)] this IEnumerable<T>? collection)
    {
        if (collection == null)
        {
            return false;
        }
        return collection.TryGetNonEnumeratedCount(out var count) ? count > 0 : collection.Any();
    }
}