using Koalakit.Primitives.DateTimes;

namespace Koalakit.Primitives.Domain;

public abstract class AuditableAggregate : AggregateRoot, IDeletable
{
    public DateTime CreatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    protected AuditableAggregate() { }

    protected AuditableAggregate(Guid id) : base(id)
    {
        CreatedAt = AppDateTime.NowUtc;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}