using KoalaKit.Modules;
using KoalaKit.Options;
using Microsoft.Extensions.Configuration;

namespace KoalaKit.Persistence.MongoDb
{
    public class MongoDbModule : KoalaModuleBase
    {
        private const string ProviderName = "MongoDb";
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            var section = koala.Configuration.GetSection($"Koala:DefaultPersistence");
            var connectionStringName = section.GetValue<string>("ConnectionStringIdentifier");
            var connectionString = section.GetValue<string>("ConnectionString");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                if (string.IsNullOrWhiteSpace(connectionStringName))
                    connectionStringName = ProviderName;

                connectionString = koala.Configuration.GetConnectionString(connectionStringName);
            }

            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = GetDefaultConnectionString();

            base.ConfigureKoala(koala);
        }
        protected virtual string GetDefaultConnectionString() => throw new Exception($"No connection string specified for the {ProviderName} provider");
    }
}