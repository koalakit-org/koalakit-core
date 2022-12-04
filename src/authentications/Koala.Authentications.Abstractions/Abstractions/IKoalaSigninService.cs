using KoalaKit.Cosmetics;

namespace Koala.Authentications
{
    public interface IKoalaSigninService
    {
        Task<KoalaSignInResult> SignIn(params KeyValuePair<string, string>[] claims);
        Task<KoalaSignInResult> SignIn(string userId);
        Task<string> ValidateSignIn(SignInParameters parameters);
        Task<string> ValidateSignIn(string token);
        Task<string> ValidRefreshToken(string token);
    }
}
