using KoalaKit.Persistence.Specifications;

namespace Koala.Authentications
{
    public class UserIdentityClaimSpecifications : EntitySpecification<UserIdentityClaim>
    {
        public UserIdentityClaimSpecifications()
        {
            AddCriteria(a => a.Deleted == false);
        }

        public UserIdentityClaimSpecifications ByUserId(string id)
        {
            AddCriteria(a => a.UserId == id);
            return this;
        }
    }
}
