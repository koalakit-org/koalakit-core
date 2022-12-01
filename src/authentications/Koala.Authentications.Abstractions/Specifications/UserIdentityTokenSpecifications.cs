using KoalaKit.Cosmetics;
using KoalaKit.Persistence.Specifications;

namespace Koala.Authentications
{
    public class UserIdentityTokenSpecifications : EntitySpecification<UserIdentityToken>
    {
        public UserIdentityTokenSpecifications()
        {
            AddCriteria(a => a.Deleted == false);
        }

        public UserIdentityTokenSpecifications ById(int id)
        {
            AddCriteria(a => a.Id == id);
            return this;
        }

        public UserIdentityTokenSpecifications ById(Guid id)
        {
            AddCriteria(a => a.ExternalId == id);
            return this;
        }

        public UserIdentityTokenSpecifications ByUserId(string id)
        {
            AddCriteria(a => a.UserId == id);
            return this;
        }

        public UserIdentityTokenSpecifications BySignInParameters(SignInParameters parameters)
        {
            AddCriteria(a => a.SignInParameters.Equals(parameters));
            return this;
        }
    }
}
