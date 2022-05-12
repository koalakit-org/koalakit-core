using KoalaKit.Modules;
using KoalaKit.Options;
using KoalaKit.Persistence.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KoalaKit.Persistence.EFCore
{
    public abstract class EfCoreModuleBase : KoalaModuleBase
    {
        protected abstract string ProviderName { get; }
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            var section = koala.Configuration.GetSection($"Koala:DefaultPersistence");
            var connectionStringName = section.GetValue<string>("ConnectionStringIdentifier");
            var connectionString = section.GetValue<string>("ConnectionString");
            var migrationsAssemblyName = section.GetValue<string>("MigrationsAssembly");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                if (string.IsNullOrWhiteSpace(connectionStringName))
                    connectionStringName = ProviderName;

                connectionString = koala.Configuration.GetConnectionString(connectionStringName);
            }

            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = GetDefaultConnectionString();
            if (string.IsNullOrWhiteSpace(migrationsAssemblyName))
                migrationsAssemblyName = GetDefaultMigrationAssemblyName();

            koala.UseEfPersistence(options => Configure(options, connectionString, string.Empty));
        }
        protected abstract void Configure(DbContextOptionsBuilder builder, string connectionString, string migrationsAssemblyName);
        protected virtual string GetDefaultConnectionString() => throw new Exception($"No connection string specified for the {ProviderName} provider");
        protected virtual string GetDefaultMigrationAssemblyName() => throw new Exception($"No migration assembly name specified");
    }
}
