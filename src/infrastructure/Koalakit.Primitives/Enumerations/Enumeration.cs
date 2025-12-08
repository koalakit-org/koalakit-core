using System.Reflection;

namespace Koalakit.Primitives.Enumerations;

public abstract class Enumeration : IEquatable<Enumeration>, IComparable<Enumeration>
{
    static readonly Dictionary<Type, object> _cache = [];
    static readonly Lock _cacheLock = new();

    public int Id { get; private set; }

    public string Name { get; protected set; }

    protected Enumeration(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Enumeration name cannot be null or empty.");

        Id = id;
        Name = name;
    }

    public override string ToString() => Name;

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

    public static T? GetByName<T>(string? name) where T : Enumeration
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return GetAll<T>()
            .FirstOrDefault(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public static bool TryGetByName<T>(string? name, out T? enumeration) where T : Enumeration
    {
        enumeration = GetByName<T>(name);
        return enumeration is not null;
    }

    public static T? GetById<T>(int id) where T : Enumeration
    {
        return GetAll<T>().FirstOrDefault(e => e.Id == id);
    }

    public static bool TryGetById<T>(int id, out T? enumeration) where T : Enumeration
    {
        enumeration = GetById<T>(id);
        return enumeration is not null;
    }

    public static bool IsValidName<T>(string? name) where T : Enumeration
    {
        return GetByName<T>(name) is not null;
    }

    public static bool IsValidId<T>(int id) where T : Enumeration
    {
        return GetById<T>(id) is not null;
    }

    public int CompareTo(Enumeration? other)
    {
        if (other is null)
            return 1;

        return Id.CompareTo(other.Id);
    }

    public bool Equals(Enumeration? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return GetType() == other.GetType() && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Id);
    }

    public static bool operator ==(Enumeration? left, Enumeration? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Enumeration? left, Enumeration? right)
    {
        return !(left == right);
    }
}