using KoalaKit.Cosmetics;

namespace Koala.Authentications
{
    public interface IKoalaSigninService
    {
        KoalaSignInResult SignIn(params KeyValuePair<string, string>[] claims);
        KoalaSignInResult SignIn(string userId);
        bool ValidateSignIn(SignInParameters parameters, out string userId);
        bool ValidateSignIn(string token);
        bool ValidRefreshToken(string token);
    }
}
