using KoalaKit.Options;
using KoalaKit.Persistence.MongoDb.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Persistence.MongoDb.Extensions
{
    public static class KoalaOptionsBuilderExtensions
    {
        public static KoalaOptionsBuilder UseMongoDbPersistence(this KoalaOptionsBuilder koala, Action<KoalaMongoDbOptions> configureOptions)
        {
            return UseMongoDbPersistence<KoalaMongoDbContext>(koala, configureOptions);
        }
        
        public static KoalaOptionsBuilder UseMongoDbPersistence<TDbContext>(this KoalaOptionsBuilder koala, Action<KoalaMongoDbOptions> configureOptions)
            where TDbContext : KoalaMongoDbContext
        {
            koala.Services.Configure(configureOptions);
            return koala;
        }
    }



}
