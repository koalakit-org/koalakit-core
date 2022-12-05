using System.Text;
using KoalaKit.Modules;
using KoalaKit.Options;
using KoalaKit.Persistence.EFCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Koala.Authentications.Jwt
{
    public class KoalaAuthenticationJwtModule : KoalaModuleBase
    {
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            koala.Services.Configure<IKoalaIdentityAuthenticationSettings>(koala.Configuration.GetSection(nameof(IKoalaIdentityAuthenticationSettings)));
            koala.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<IKoalaIdentityAuthenticationSettings>>().Value);

            koala.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            koala.Services.AddScoped<IKoalaIdentityService, KoalaIdentityService>();
            koala.Services.AddSingleton<IKoalaJwtGenerator, KoalaJwtGenerator>();
            koala.Services.AddSingleton<IKoalaSigninService, JwtKoalaSigninService>();

            AddJwtAuthentication(koala);

            EntityProvidersCollection.AddDbEntityProvider(typeof(AuthenticationEntityProvider).Assembly);
            base.ConfigureKoala(koala);
        }

        private void AddJwtAuthentication(KoalaOptionsBuilder koala)
        {
            var settings = koala.Configuration.GetSection(nameof(IKoalaIdentityAuthenticationSettings)).Get<IKoalaIdentityAuthenticationSettings>();

            if (settings == null || settings == default) return;

            var key = Encoding.ASCII.GetBytes(settings.Secret);
            koala.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("Token-Expired", "true");
                        return Task.CompletedTask;
                    }
                };

            });

        }
    }
}
