using System.Reflection;

namespace Koalakit.Primitives.Enumerations;

public abstract class Enumeration : IEquatable<Enumeration, TKey>, IComparable<Enumeration>
{
    private static readonly Dictionary<Type, object> _cache = new();
    private static readonly object _cacheLock = new();

    public int Id { get; private set; }

    public string Name { get; protected set; }

    protected Enumeration(TKey id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Enumeration name cannot be null or empty.");

        Id = id;
        Name = name;
    }

    /// <summary>
    /// Returns the name of this enumeration value.
    /// </summary>
    public override string ToString() => Name;

    /// <summary>
    /// Gets all values of the specified enumeration type.
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <returns>All enumeration values of the specified type.</returns>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var type = typeof(T);

        lock (_cacheLock)
        {
            if (!_cache.TryGetValue(type, out var cached))
            {
                var fields = type.GetFields(BindingFlags.Public |
                                           BindingFlags.Static |
                                           BindingFlags.DeclaredOnly);

                cached = fields
                    .Select(f => f.GetValue(null))
                    .Cast<T>()
                    .OrderBy(e => e.Id)
                    .ToList();

                _cache[type] = cached;
            }

            return (IEnumerable<T>)cached;
        }
    }

    /// <summary>
    /// Gets an enumeration value by its name (case-insensitive).
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <param name="name">The name to search for.</param>
    /// <returns>The enumeration value, or null if not found.</returns>
    public static T? GetByName<T>(string? name) where T : Enumeration
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return GetAll<T>()
            .FirstOrDefault(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Tries to get an enumeration value by its name (case-insensitive).
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <param name="name">The name to search for.</param>
    /// <param name="enumeration">The found enumeration value, or null if not found.</param>
    /// <returns>True if found, false otherwise.</returns>
    public static bool TryGetByName<T>(string? name, out T? enumeration) where T : Enumeration
    {
        enumeration = GetByName<T>(name);
        return enumeration is not null;
    }

    /// <summary>
    /// Gets an enumeration value by its Id.
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <param name="id">The Id to search for.</param>
    /// <returns>The enumeration value, or null if not found.</returns>
    public static T? GetById<T>(int id) where T : Enumeration
    {
        return GetAll<T>().FirstOrDefault(e => e.Id == id);
    }

    /// <summary>
    /// Tries to get an enumeration value by its Id.
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <param name="id">The Id to search for.</param>
    /// <param name="enumeration">The found enumeration value, or null if not found.</param>
    /// <returns>True if found, false otherwise.</returns>
    public static bool TryGetById<T>(int id, out T? enumeration) where T : Enumeration
    {
        enumeration = GetById<T>(id);
        return enumeration is not null;
    }

    /// <summary>
    /// Checks if the given name is a valid enumeration value name (case-insensitive).
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <param name="name">The name to validate.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public static bool IsValidName<T>(string? name) where T : Enumeration
    {
        return GetByName<T>(name) is not null;
    }

    /// <summary>
    /// Checks if the given Id is a valid enumeration value Id.
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <param name="id">The Id to validate.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public static bool IsValidId<T>(int id) where T : Enumeration
    {
        return GetById<T>(id) is not null;
    }

    /// <summary>
    /// Compares this enumeration to another based on their Ids.
    /// </summary>
    public int CompareTo(Enumeration? other)
    {
        if (other is null)
            return 1;

        return Id.CompareTo(other.Id);
    }

    /// <summary>
    /// Determines whether the specified enumeration is equal to the current enumeration.
    /// </summary>
    public bool Equals(Enumeration? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return GetType() == other.GetType() && Id == other.Id;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current enumeration.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is Enumeration other && Equals(other);
    }

    /// <summary>
    /// Returns the hash code for this enumeration.
    /// </summary>
    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Id);
    }

    /// <summary>
    /// Equality operator.
    /// </summary>
    public static bool operator ==(Enumeration? left, Enumeration? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator.
    /// </summary>
    public static bool operator !=(Enumeration? left, Enumeration? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Less than operator.
    /// </summary>
    public static bool operator <(Enumeration? left, Enumeration? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Less than or equal operator.
    /// </summary>
    public static bool operator <=(Enumeration? left, Enumeration? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Greater than operator.
    /// </summary>
    public static bool operator >(Enumeration? left, Enumeration? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Greater than or equal operator.
    /// </summary>
    public static bool operator >=(Enumeration? left, Enumeration? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}

