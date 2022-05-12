using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.Oracle
{
    internal static class OracleDbContextOptionsBuilderExtensions
    {
        internal static DbContextOptionsBuilder ConfigureOracle(this DbContextOptionsBuilder builder,
            string connectionString, string migrationsAssemblyName)
            => builder.UseOracle(connectionString, db => db
                .MigrationsAssembly(migrationsAssemblyName)
                .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema));
    }
}
