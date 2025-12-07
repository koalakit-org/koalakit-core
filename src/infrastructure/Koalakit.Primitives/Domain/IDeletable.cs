namespace Koalakit.Primitives.Domain;

public interface IDeletable
{
    bool IsDeleted { get; }
    void Restore();
}