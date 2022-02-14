using Microsoft.EntityFrameworkCore;

namespace KoalaKit.Persistence.EFCore.PostgreSql
{
    internal class PostgreSqlModule : EfCoreModuleBase
    {
        protected override string ProviderName => "PostgreSql";

        protected override void Configure(DbContextOptionsBuilder builder, string connectionString)
            => builder.UsePostgreSql(connectionString);
    }
}
