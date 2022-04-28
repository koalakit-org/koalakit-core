namespace KoalaKit.Persistence
{
    public interface IKoalaEntity
    {
        int Id { get; set; }
        Guid ExternalId { get; set; }
    }
}
