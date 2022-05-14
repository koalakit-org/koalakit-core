using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KoalaKit.Persistence.EFCore.SqlServer
{
    public class SqlServerKoalaDbContextFactory : IDesignTimeDbContextFactory<KoalaDbContext>
    {
        public KoalaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<KoalaDbContext>();
            var connectionString = args.Any() ? args[0] : throw new InvalidOperationException("");
            var migrationsAssemblyName = args.Any() ? args[1] : throw new InvalidOperationException("");
            builder.ConfigureSqlServer(connectionString, migrationsAssemblyName);
            return new KoalaDbContext(builder.Options);
        }
    }
}
