using KoalaKit.Extensions;
using KoalaKit.Persistence.EfCore.Test.Models;
using KoalaKit.Persistence.EFCore;
using KoalaKit.Persistence.EFCore.Sqlite;
using KoalaKit.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoalaKit.Persistence.EfCore.Test.Tests
{
    public class SqliteTests : TestContainerBase
    {
        private IDbContextFactory<KoalaDbContext>? contextFactory;
        private IStore<TestModelChild>? childrenStore;

        [TestMethod]
        //TODO: use Migration task
        public void Sqlite_MigrateDb()
        {
            if (contextFactory is null)
                Assert.Fail("persistence Sqlite module not well registered. IDbContextFactory is null");

            using var dbContext = contextFactory.CreateDbContext();

            dbContext.Database.EnsureCreated();
            Assert.IsTrue(true, "Sqlite database schema created");
        }

        [TestMethod]
        public void Sqlite_InsertRecord()
        {
            if (childrenStore is null) Assert.Fail("Store registration failed");
            childrenStore?.AddAsync(new TestModelChild
            {
                Name = "Test Name",
                Parent = new TestModelParent
                {
                    Name = "Test Name",
                }
            }).Wait();
            Assert.IsTrue(true, "Sqlite, Items added successfully!");
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddKoalaKitCore(builder => builder.AddModules(typeof(SqliteModule)));
            EntityProvidersCollection.AddDbEntityProvider(typeof(TestDbEntityProvider).Assembly);
            base.ConfigureServices(services);
        }

        protected override void ResolveServices()
        {
            base.ResolveServices();
            var scopedProvider = ServiceProvider.CreateScope().ServiceProvider;
            contextFactory = scopedProvider.GetService<IDbContextFactory<KoalaDbContext>>();
            childrenStore = ServiceProvider.GetService<IStore<TestModelChild>>();
        }
    }
}
