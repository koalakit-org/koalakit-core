namespace Kamel.Primitives.Domain;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public int Flag { get; private set; }
    protected Entity() { }

    protected Entity(Guid id)
    {
        if (id == default)
            throw new ArgumentNullException(nameof(id));
        Id = id;
    }

    protected bool HasFlag(Enum flag) => (Flag & Convert.ToInt32(flag)) == Convert.ToInt32(flag);
    protected void AddFlag(Enum flag) => Flag |= Convert.ToInt32(flag);
    protected void RemoveFlag(Enum flag) => Flag &= ~Convert.ToInt32(flag);
    protected void ToggleFlag(Enum flag) => Flag ^= Convert.ToInt32(flag);
    public void SetFlag(Enum flag) => Flag = Convert.ToInt32(flag);
    public void ClearFlags() => Flag = 0;

    public static bool operator ==(Entity? first, Entity? second)
    {
        return Equals(first, second);
    }

    public static bool operator !=(Entity? first, Entity? second)
    {
        return !Equals(first, second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        return Id == other.Id && GetType() == other.GetType();
    }

    public override int GetHashCode() =>
        HashCode.Combine(Id, GetType());

    public override string ToString() =>
        $"{GetType().Name} {Id}";
}