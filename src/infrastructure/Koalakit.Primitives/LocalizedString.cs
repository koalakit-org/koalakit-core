namespace Kamel.Primitives;

public struct LocalizedString
{
    public string Key { get; }
    public string? Value { get; private set; }

    public LocalizedString(string key)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        Value = null;
    }

    public LocalizedString(string key, string? value)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        Value = value;
    }

    public override readonly string ToString() => Value ?? Key;
    public static implicit operator LocalizedString(string key) => new(key);
    public static implicit operator string(LocalizedString localizedString) => localizedString.OrKey();

    public override readonly bool Equals(object? obj) => obj is LocalizedString other && Key == other.Key;
    public override readonly int GetHashCode() => Key.GetHashCode();
    public void SetValue(string value) => Value = value;

    public readonly string OrKey() => Value ?? Key;
    public readonly bool IsResolved => Value is not null;
    public static bool operator ==(LocalizedString left, LocalizedString right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(LocalizedString left, LocalizedString right)
    {
        return !(left==right);
    }
}