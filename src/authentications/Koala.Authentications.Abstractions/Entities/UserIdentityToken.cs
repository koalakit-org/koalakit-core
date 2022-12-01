using KoalaKit.Cosmetics;
using KoalaKit.Persistence;

namespace Koala.Authentications
{
    public class UserIdentityToken : IKoalaEntity
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }
        public bool Deleted { get; set; }
        public string UserId { get; set; }
        public UserIdentityTokenStatusEnum Status { get; set; }
        public SignInParameters SignInParameters { get; set; }
    }
}
