using KoalaKit.Cosmetics;

namespace Koala.Authentications
{
    public interface IKoalaSigninService
    {
        Task<KoalaSignInResult> SignIn(string userId);
        Task<KoalaSignInResult> ValidateSignIn(SignInParameters parameters);
        Task<KoalaSignInResult> ValidateSignIn(string refreshToken);
    }
}
