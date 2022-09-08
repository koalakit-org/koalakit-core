using KoalaKit.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Koalakit.Sample.SimpleApi.Entities.DbProviders
{
    public class ForecastSqLDbProvider : IDbEntityProvider
    {
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forecast>(entity =>
            {
                entity.ToTable("Forecasts");

                entity.HasKey(f => f.Id);
                entity.Property(f => f.ExternalId).IsRequired();
            });
        }
    }
}
