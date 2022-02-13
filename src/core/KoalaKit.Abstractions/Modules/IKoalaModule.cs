using KoalaKit.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace KoalaKit.Modules
{
    internal interface IKoalaModule
    {
        void ConfigureApp(IApplicationBuilder app);
        void ConfigureKoala(KoalaOptionsBuilder koala, IConfiguration configuration);
    }
}
