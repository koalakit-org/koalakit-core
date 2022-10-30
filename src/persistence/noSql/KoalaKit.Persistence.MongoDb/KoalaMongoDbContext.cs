using KoalaKit.Persistence.MongoDb.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KoalaKit.Persistence.MongoDb
{
    public class KoalaMongoDbContext
    {
        public KoalaMongoDbContext(IOptions<KoalaMongoDbOptions> options)
        {
            var connectionString = options.Value.ConnectionString;
            var mongoClient = new MongoClient(connectionString);
            var databaseName = options.Value.DatabaseName is not null and not "" ? options.Value.DatabaseName : MongoUrl.Create(connectionString).DatabaseName;

            if (databaseName == null)
                throw new Exception("Please specify a database name, either via the connection string or via the DatabaseName setting.");

            MongoDatabase = mongoClient.GetDatabase(databaseName);
        }

        protected IMongoDatabase MongoDatabase { get; }
    }
}
