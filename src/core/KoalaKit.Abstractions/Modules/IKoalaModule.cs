using KoalaKit.Options;
using Microsoft.AspNetCore.Builder;

namespace KoalaKit.Modules
{
    internal interface IKoalaModule
    {
        void ConfigureApp(IApplicationBuilder app);
        void ConfigureKoala(KoalaOptionsBuilder koala);
    }
}
