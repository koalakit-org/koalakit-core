using KoalaKit.Modules;
using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Koala.Authentications.Jwt
{
    public class KoalaAuthenticationJwtModule : KoalaModuleBase
    {
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            koala.Services.AddSingleton<IKoalaJwtGenerator, KoalaJwtGenerator>();
            koala.Services.AddSingleton<IKoalaSigninService, JwtKoalaSigninService>();
            base.ConfigureKoala(koala);
        }
    }
}
