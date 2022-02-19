using KoalaKit.Extensions;
using KoalaKit.Persistence.EfCore.Test.Models;
using KoalaKit.Persistence.EFCore;
using KoalaKit.Persistence.EFCore.SqlServer;
using KoalaKit.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoalaKit.Persistence.EfCore.Test.Tests
{
    [TestClass]
    public class SqlServerTests : TestContainerBase
    {
        private IDbContextFactory<KoalaDbContext>? contextFactory;
        private IStore<TestModelChild>? childrenStore;

        [TestMethod]
        public void SqlServer_MigrateDb()
        {
            if (contextFactory is null)
                Assert.Fail("persistence SqlServer module not well registered. IDbContextFactory is null");

            using var dbContext = contextFactory.CreateDbContext();

            dbContext.Database.EnsureCreated();
            Assert.IsTrue(true, "Sql server database schema created");
        }


        [TestMethod]
        public void SqlServer_InsertRecord()
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
            Assert.IsTrue(true, "SqlServer, Items added successfully!");
        }


        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddKoalaKitCore(builder => builder.AddModules(typeof(SqlServerModule)));
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
