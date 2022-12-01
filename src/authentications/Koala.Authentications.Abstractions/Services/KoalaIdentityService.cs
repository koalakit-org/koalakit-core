using KoalaKit.Cosmetics;
using KoalaKit.Persistence;

namespace Koala.Authentications
{
    ///TODO: re-write after IStore update method is done.
    public class KoalaIdentityService : IKoalaIdentityService
    {
        private readonly IStore<UserIdentityToken> tokensStore;
        private readonly IStore<UserIdentityClaim> claimsStore;

        public KoalaIdentityService(IStore<UserIdentityToken> tokensStore, IStore<UserIdentityClaim> claimsStore)
        {
            this.tokensStore = tokensStore;
            this.claimsStore = claimsStore;
        }

        public async Task Add(IdentityAddParameters parameters)
        {
            foreach (var identifier in parameters.Identifiers)
            {
                var singIn = new SignInParameters(identifier, parameters.Password);

                await tokensStore.AddAsync(new UserIdentityToken
                {
                    UserId = parameters.UserId,
                    SignInParameters = singIn,
                    Status = UserIdentityTokenStatusEnum.Active,
                });
            }

            foreach (var claim in parameters.Claims)
            {
                await claimsStore.AddAsync(new UserIdentityClaim
                {
                    UserId = parameters.UserId,
                    Key = claim.Key,
                    Value = claim.Value,
                });
            }
        }

        public async Task AddOrUpdate(IdentityAddParameters parameters)
        {
            await Remove(parameters.UserId);
            await Add(parameters);
        }

        public async Task Block(string userId)
        {
            var tokens = await tokensStore.ListAsync(new UserIdentityTokenSpecifications().ByUserId(userId));
            foreach (var token in tokens)
            {
                await tokensStore.UpdateAsync(new UserIdentityTokenSpecifications().ById(token.Id),
                    async t =>
                    {
                        if (t != null)
                        {
                            t.Status = UserIdentityTokenStatusEnum.Blocked;
                        }
                    });
            }
        }

        public async Task Remove(string userId)
        {
            var tokens = await tokensStore.ListAsync(new UserIdentityTokensSpecification().ByUserId(userId));
            foreach (var token in tokens)
            {
                await tokensStore.UpdateAsync(new UserIdentityTokensSpecification().ById(token.Id),
                    async t =>
                    {
                        if (t != null)
                        {
                            t.Deleted = true;
                        }
                    });
            }
        }
    }
}
