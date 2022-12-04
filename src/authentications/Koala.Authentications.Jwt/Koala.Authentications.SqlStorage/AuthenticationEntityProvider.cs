using KoalaKit.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Koala.Authentications.SqlStorage
{
    public class AuthenticationEntityProvider : IDbEntityProvider
    {
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserIdentityToken>(entity =>
            {
                entity.ToTable("UserIdentityTokens");
                entity.Property(a => a.UserId).IsRequired(true);
                entity.Property(a => a.SignInParameters).HasConversion<SignInParametersSqlConverter>();
            });

            modelBuilder.Entity<UserIdentityClaim>(entity =>
            {
                entity.ToTable("UserIdentityClaims");
                entity.Property(a => a.UserId).IsRequired(true);
                entity.Property(a => a.Key).IsRequired(true);
                entity.Property(a => a.Value).IsRequired(true);
            });
        }
    }
}
