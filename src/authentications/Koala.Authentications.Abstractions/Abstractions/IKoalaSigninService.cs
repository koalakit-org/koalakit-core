using KoalaKit.Cosmetics;

namespace Koala.Authentications
{
    public interface IKoalaSigninService
    {
        Task<KoalaSignInResult> SignIn(string userId);
        Task<KoalaSignInResult> ValidateAndSignIn(SignInParameters parameters);
        Task<KoalaSignInResult> ValidateRegistrationToken(string refreshToken);
        Task<KoalaSignInResult> ValidateRefreshToken(string refreshToken);
    }
}
