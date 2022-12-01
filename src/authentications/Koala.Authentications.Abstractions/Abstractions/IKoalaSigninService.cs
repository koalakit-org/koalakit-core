using KoalaKit.Cosmetics;

namespace Koala.Authentications
{
    public interface IKoalaSigninService
    {
        Task<KoalaSignInResult> SignIn(params KeyValuePair<string, string>[] claims);
        Task<KoalaSignInResult> SignIn(string userId);
        Task<KoalaSignInResult> ValidateSignIn(SignInParameters parameters);
        Task<KoalaSignInResult> ValidateSignIn(string token);
        KoalaSignInResult ValidRefreshToken(string token);
    }
}
