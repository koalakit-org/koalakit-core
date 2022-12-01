using KoalaKit.Persistence;

namespace Koala.Authentications
{
    public class UserIdentityClaim : IKoalaEntity
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }
        public bool Deleted { get; set; }
        public string UserId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
