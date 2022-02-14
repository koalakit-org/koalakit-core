using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.SqlServer
{
    public static class DbContextOptionsBuilderExtensions
    {
        internal static DbContextOptionsBuilder UseSqlServer(this DbContextOptionsBuilder builder, string connectionString)
            => builder.UseSqlServer(connectionString, typeof(SqlServerKoalaDbContextFactory).Assembly);


        internal static DbContextOptionsBuilder UseSqlServer(this DbContextOptionsBuilder builder, string connectionString, Assembly assembly)
            => builder.UseSqlServer(connectionString,db => db
                    .MigrationsAssembly(assembly.GetName().Name)
                    .MigrationsHistoryTable(KoalaDbContext.MigrationsHistoryTable, KoalaDbContext.Schema));
    }
}
