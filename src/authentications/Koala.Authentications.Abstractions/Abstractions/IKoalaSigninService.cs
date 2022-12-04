using KoalaKit.Cosmetics;

namespace Koala.Authentications
{
    public interface IKoalaSigninService
    {
        Task<KoalaSignInResult> GetToken(string userId);
        Task<KoalaSignInResult> SignIn(SignInParameters parameters);
        Task<KoalaSignInResult> SignIn(string refreshToken);
    }
}
