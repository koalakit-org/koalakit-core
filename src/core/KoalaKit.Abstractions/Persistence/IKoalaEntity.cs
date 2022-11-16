namespace KoalaKit.Persistence
{
    public interface IKoalaEntity
    {
        int Id { get; set; }
        Guid ExternalId { get; set; }
        DateTime CreatedUtc { get; set; }
        DateTime? UpdatedUtc { get; set; }
        bool Deleted { get; set; }
    }
}
