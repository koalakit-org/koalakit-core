using System.Security.Claims;
using KoalaKit.Cosmetics;
using KoalaKit.Persistence;

namespace Koala.Authentications.Jwt
{
    public class JwtKoalaSigninService : IKoalaSigninService
    {
        private readonly IStore<UserIdentityToken> tokensStore;
        private readonly IStore<UserIdentityClaim> claimsStore;
        private readonly ITokenizationService tokenizationService;
        private readonly IKoalaJwtGenerator jwtGenerator;

        public JwtKoalaSigninService(
            IStore<UserIdentityToken> tokensStone,
            IStore<UserIdentityClaim> claimsStore,
            ITokenizationService tokenizationService,
            IKoalaJwtGenerator jwtGenerator)
        {
            this.tokensStore = tokensStone;
            this.claimsStore = claimsStore;
            this.tokenizationService = tokenizationService;
            this.jwtGenerator = jwtGenerator;
        }

        public async Task<KoalaSignInResult> SignIn(params KeyValuePair<string, string>[] claims)
        {
            if (claims == null) return new KoalaSignInResult("not-allowed");

            var token = jwtGenerator.GenerateAccessToken(claims.Select(a => new Claim(a.Key, a.Value)).ToArray());
            var refreshToken = jwtGenerator.GenerateRefreshToken(Guid.NewGuid().ToString());
            return new KoalaSignInResult(token, refreshToken);
        }

        public async Task<KoalaSignInResult> SignIn(string userId)
        {
            var claims = await claimsStore.ListAsync(new UserIdentityClaimSpecifications().ByUserId(userId));
            if (claims == null || !claims.Any())
            {
                return new KoalaSignInResult("not-allowed");
            }
            return await SignIn(claims.Select(a => new KeyValuePair<string, string>(a.Key, a.Value)).ToArray());
        }

        public async Task<string> ValidateSignIn(SignInParameters parameters)
        {
            var token = await tokensStore.FindAsync(new UserIdentityTokenSpecifications().BySignInParameters(parameters));
            if (token == null)
            {
                return string.Empty;
            }
            return token.UserId;
        }

        public Task<string> ValidateSignIn(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ValidRefreshToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
