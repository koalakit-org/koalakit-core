using System.Reflection;

namespace Koalakit.Primitives.Enumerations;

public abstract class Enumeration<TId> :
    IEquatable<Enumeration<TId>>, IComparable<Enumeration<TId>>
    where TId : IComparable<TId>
{
    static readonly Dictionary<Type, object> _cache = [];
    static readonly Lock _cacheLock = new();

    public TId Id { get; }
    public string Name { get; }

    protected Enumeration(TId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Enumeration name cannot be null or empty.");

        Id = id;
        Name = name;
    }

    public override string ToString() => Name;

    public static IEnumerable<TEnum> GetAll<TEnum>() where TEnum : Enumeration<TId>
    {
        var type = typeof(TEnum);

        lock (_cacheLock)
        {
            if (!_cache.TryGetValue(type, out var cached))
            {
                var fields = type.GetFields(BindingFlags.Public |
                                            BindingFlags.Static |
                                            BindingFlags.DeclaredOnly);

                cached = fields
                    .Select(f => f.GetValue(null))
                    .Cast<TEnum>()
                    .OrderBy(e => e.Id)
                    .ToList();

                _cache[type] = cached;
            }

            return (IEnumerable<TEnum>)cached;
        }
    }

    public static TEnum? GetByName<TEnum>(string? name) where TEnum : Enumeration<TId>
    {
        if (string.IsNullOrWhiteSpace(name))
            return default;

        return GetAll<TEnum>()
            .FirstOrDefault(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public static bool TryGetByName<TEnum>(string? name, out TEnum? enumeration)
        where TEnum : Enumeration<TId>
    {
        enumeration = GetByName<TEnum>(name);
        return enumeration is not null;
    }

    public static TEnum? GetById<TEnum>(TId id) where TEnum : Enumeration<TId>
    {
        return GetAll<TEnum>().FirstOrDefault(e => e.Id.CompareTo(id) == 0);
    }

    public static bool TryGetById<TEnum>(TId id, out TEnum? enumeration)
        where TEnum : Enumeration<TId>
    {
        enumeration = GetById<TEnum>(id);
        return enumeration is not null;
    }

    public static bool IsValidName<TEnum>(string? name) where TEnum : Enumeration<TId>
    {
        return GetByName<TEnum>(name) is not null;
    }

    public static bool IsValidId<TEnum>(TId id) where TEnum : Enumeration<TId>
    {
        return GetById<TEnum>(id) is not null;
    }

    public int CompareTo(Enumeration<TId>? other)
    {
        if (other is null)
            return 1;

        return Id.CompareTo(other.Id);
    }

    public bool Equals(Enumeration<TId>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return GetType() == other.GetType() &&
               Id.CompareTo(other.Id) == 0;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TId> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Id);
    }

    public static bool operator ==(Enumeration<TId>? left, Enumeration<TId>? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Enumeration<TId>? left, Enumeration<TId>? right)
    {
        return !(left == right);
    }
}
