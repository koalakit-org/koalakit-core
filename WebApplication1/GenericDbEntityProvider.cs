using KoalaKit.Persistence;
using KoalaKit.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class GenericDbEntityProvider : IDbEntityProvider
    {
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.ExternalId).IsRequired();
            });
        }
    }

    public class User : IKoalaEntity
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
    }
}
