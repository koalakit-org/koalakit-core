using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.SqlServer
{
    internal static class SqlServerDbContextOptionsBuilderExtensions
    {
        internal static DbContextOptionsBuilder ConfigureSqlServer(this DbContextOptionsBuilder builder, string connectionString, string migrationsAssemblyName)
        {
            Console.WriteLine("*************** ConfigureSqlServer ***************");
            return builder.UseSqlServer(connectionString, db => db
                     .MigrationsAssembly(migrationsAssemblyName)
                     .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema));
        }
    }
}
