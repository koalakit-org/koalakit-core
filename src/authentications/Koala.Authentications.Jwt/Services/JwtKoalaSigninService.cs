using System.Security.Claims;
using KoalaKit.Cosmetics;
using KoalaKit.Persistence;

namespace Koala.Authentications.Jwt
{
    public class JwtKoalaSigninService : IKoalaSigninService
    {
        private readonly IStore<UserIdentityToken> tokensStore;
        private readonly IStore<UserIdentityClaim> claimsStore;
        private readonly IKoalaJwtGenerator jwtGenerator;

        public JwtKoalaSigninService(
            IStore<UserIdentityToken> tokensStone,
            IStore<UserIdentityClaim> claimsStore,
            IKoalaJwtGenerator jwtGenerator)
        {
            this.tokensStore = tokensStone;
            this.claimsStore = claimsStore;
            this.jwtGenerator = jwtGenerator;
        }

        public async Task<KoalaSignInResult> SignIn(string userId)
        {
            var claims = await claimsStore.ListAsync(new UserIdentityClaimSpecifications().ByUserId(userId));
            if (claims == null || !claims.Any())
            {
                return new KoalaSignInResult("not-allowed");
            }
            var accessToken = jwtGenerator.GenerateAccessToken(claims.Select(a => new Claim(a.Key, a.Value)).ToArray());
            var refreshToken = jwtGenerator.GenerateRefreshToken(userId);
            return new KoalaSignInResult(accessToken, refreshToken);
        }

        public async Task<KoalaSignInResult> ValidateSignIn(SignInParameters parameters)
        {
            var identityToken = await tokensStore.FindAsync(new UserIdentityTokenSpecifications().BySignInParameters(parameters));
            if (identityToken == null)
            {
                return new KoalaSignInResult("invalid-parameters");
            }

            return await SignIn(identityToken.UserId);
        }

        public async Task<KoalaSignInResult> ValidateSignIn(string refreshToken)
        {
            if (!jwtGenerator.ValidateRefreshToken(refreshToken, out var userId))
            {
                return new KoalaSignInResult("not-allowed");
            }
            return await SignIn(userId);
        }
    }
}
