using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.PostgreSql
{
    internal static class DbContextOptionsBuilderExtensions
    {
        internal static DbContextOptionsBuilder UsePostgreSql(this DbContextOptionsBuilder builder,
            string connectionString)
        => builder.UsePostgreSql(connectionString, typeof(PostgreSqlKoalaContextFactory).Assembly);


        internal static DbContextOptionsBuilder UsePostgreSql(this DbContextOptionsBuilder builder,
            string connectionString, Assembly assembly) 
            => builder.UseNpgsql(connectionString, db => db
            .MigrationsAssembly(assembly.GetName().Name)
            .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema));
    }
}
