using KoalaKit.Extensions;
using KoalaKit.Persistence.EfCore.Test.Models;
using KoalaKit.Persistence.EFCore;
using KoalaKit.Persistence.EFCore.MySql;
using KoalaKit.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoalaKit.Persistence.EfCore.Test.Tests
{
    [TestClass]
    public class MySqlTests : TestContainerBase
    {
        private IDbContextFactory<KoalaDbContext>? contextFactory;
        private IStore<TestModelChild>? childrenStore;

        [TestMethod]
        //TODO: use Migration task
        public void MySql_MigrateDb()
        {
            if (contextFactory is null)
                Assert.Fail("persistence MySql module not well registered. IDbContextFactory is null");

            using var dbContext = contextFactory.CreateDbContext();

            dbContext.Database.EnsureCreated();
            Assert.IsTrue(true, "MySql database schema created");
        }

        [TestMethod]
        public void MySql_InsertRecord()
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
            Assert.IsTrue(true, "MySql, Items added successfully!");
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddKoalaKit(builder => builder.AddModules(typeof(MySqlModule)));
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