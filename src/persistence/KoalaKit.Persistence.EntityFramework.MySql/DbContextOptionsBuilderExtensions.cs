using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace KoalaKit.Persistence.EFCore.MySql
{
    internal static class DbContextOptionsBuilderExtensions
    {
        internal static DbContextOptionsBuilder UseMySql(this DbContextOptionsBuilder builder, string connectionString)
            => builder.UseMySql(connectionString, typeof(MySqlKoalaContextFactory).Assembly);


        internal static DbContextOptionsBuilder UseMySql(this DbContextOptionsBuilder builder, string connectionString, Assembly assembly)
            => builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                db => db
                    .MigrationsAssembly(assembly.GetName().Name)
                    .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema)
                    .SchemaBehavior(MySqlSchemaBehavior.Ignore));
    }
}
