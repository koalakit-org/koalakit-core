using System.Reflection;

namespace Koalakit.Primitives.Extensions;

public static class TypeExtensions
{
    public static Assembly[] ToAssemblies(this Type[] types)
        => [.. types.Select(t => t.Assembly).Distinct()];
}