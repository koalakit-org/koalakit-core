using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.Oracle
{
    internal static class DbContextOptionsBuilderExtensions
    {
        internal static DbContextOptionsBuilder UseOracle(this DbContextOptionsBuilder builder,
            string connectionString)
            => builder.UseOracle(connectionString, typeof(OracleKoalaContextFactory).Assembly);


        internal static DbContextOptionsBuilder UseOracle(this DbContextOptionsBuilder builder,
            string connectionString, Assembly assembly)
            => builder.UseOracle(connectionString, db => db
                .MigrationsAssembly(assembly.GetName().Name)
                .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema));

    }
}
