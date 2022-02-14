using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KoalaKit.Persistence.EFCore.MySql
{
    public class MySqlKoalaContextFactory : IDesignTimeDbContextFactory<KoalaDbContext>
    {
        public KoalaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<KoalaDbContext>();
            var connectionString = args.Any() ? args[0] : throw new InvalidOperationException("");
            builder.UseMySql(connectionString);
            return new KoalaDbContext(builder.Options);
        }
    }
}
