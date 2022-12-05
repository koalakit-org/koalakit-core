using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using KoalaKit.Options;
using KoalaKit.Persistence.EFCore.DbFactoryServices;
using KoalaKit.Persistence.EFCore.Stores;
using KoalaKit.Persistence.EFCore.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Persistence.EFCore.Extensions
{
    public static class KoalaOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures the application to use Entity Framework Core for persistence, using pooled DB Context instances.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Pooled DB Context instances is a performance optimization which is documented in more detail at
        /// https://docs.microsoft.com/en-us/ef/core/performance/advanced-performance-topics?tabs=with-constant#dbcontext-pooling.
        /// </para>
        /// </remarks>
        /// <param name="koala">An options builder</param>
        /// <param name="configure">A configuration builder callback</param>
        /// <param name="autoRunMigrations">If <c>true</c> then database migrations will be auto-executed on startup</param>
        /// <returns>The Koala options builder, so calls may be chained</returns>
        public static KoalaOptionsBuilder UseEfPersistence(this KoalaOptionsBuilder koala,
            Action<DbContextOptionsBuilder> configure, bool autoRunMigrations = true)
        {
            return koala.UseEfPersistence<KoalaDbContext>(configure, autoRunMigrations);
        }

        public static KoalaOptionsBuilder UseEfPersistence<TKoalaContext>(this KoalaOptionsBuilder koala,
            Action<DbContextOptionsBuilder> configure, bool autoRunMigrations = true)
            where TKoalaContext : KoalaDbContext
        {
            return koala.UseEfPersistence<TKoalaContext>((_, builder) => configure(builder), autoRunMigrations);
        }


        public static KoalaOptionsBuilder UseEfPersistence<TKoalaContext>(this KoalaOptionsBuilder koala,
            Action<IServiceProvider, DbContextOptionsBuilder> configure, bool autoRunMigrations = true)
            where TKoalaContext : KoalaDbContext
        {
            return UseEfPersistence<TKoalaContext>(koala, configure, autoRunMigrations, true, ServiceLifetime.Singleton);
        }

        public static KoalaOptionsBuilder UseEfPersistence<TKoalaContext>(KoalaOptionsBuilder koala,
            Action<IServiceProvider, DbContextOptionsBuilder> configure, bool autoRunMigrations, bool useContextPooling,
            ServiceLifetime serviceLifetime)
            where TKoalaContext : KoalaDbContext
        {
            if (useContextPooling)
            {
                koala.Services.AddPooledDbContextFactory<TKoalaContext>(configure);
            }
            else
            {
                koala.Services.AddDbContextFactory<TKoalaContext>(configure, serviceLifetime);
            }
            if (autoRunMigrations)
            {
                koala.Services.AddKoalaTask<CodFirstMigrationsTask>();
            }

            koala.Services.AddScoped(typeof(IStore<>), typeof(KoalaEntityFrameworkStore<>));
            koala.Services.AddSingleton<IKoalaContextFactory, KoalaContextFactory<TKoalaContext>>();
            return koala;
        }

        public static string GetConnectionString(this KoalaOptionsBuilder koala, string provider)
        {
            var section = koala.Configuration.GetSection($"Koala:DefaultPersistence");
            var connectionStringName = section.GetValue<string>("ConnectionStringIdentifier");
            var connectionString = section.GetValue<string>("ConnectionString");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                if (string.IsNullOrWhiteSpace(connectionStringName))
                    connectionStringName = provider;

                connectionString = koala.Configuration.GetConnectionString(connectionStringName);
            }

            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = GetDefaultConnectionString(provider);

            return connectionString;
        }

        public static string GetMigrationAssemblyName(this KoalaOptionsBuilder koala)
        {
            var section = koala.Configuration.GetSection($"Koala:DefaultPersistence");
            var migrationsAssemblyName = section.GetValue<string>("MigrationsAssembly");

            if (string.IsNullOrWhiteSpace(migrationsAssemblyName))
                migrationsAssemblyName = GetDefaultMigrationAssemblyName();


            return migrationsAssemblyName;
        }

        private static string GetDefaultConnectionString(string provider) => throw new Exception($"No connection string specified for the {provider} provider");
        private static string GetDefaultMigrationAssemblyName() => throw new Exception($"No migration assembly name specified");
    }
}
