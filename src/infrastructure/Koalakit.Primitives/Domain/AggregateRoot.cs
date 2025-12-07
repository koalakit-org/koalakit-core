namespace Kamel.Primitives.Domain;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot() { }
    protected AggregateRoot(Guid id) : base(id) { }
}