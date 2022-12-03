using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<KoalaSignInResult> ValidateSignIn(SignInParameters parameters)
        {
            var token = await tokensStore.FindAsync(new UserIdentityTokenSpecifications().BySignInParameters(parameters));
            if (token == null)
            {
                return new KoalaSignInResult("invalid-parameters");
            }
            return new KoalaSignInResult(token.UserId);
        }

        public Task<KoalaSignInResult> ValidateSignIn(string token)
        {
            throw new NotImplementedException();
        }

        public KoalaSignInResult ValidRefreshToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
