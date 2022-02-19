using KoalaKit.Extensions;
using KoalaKit.Persistence.EfCore.Test.Models;
using KoalaKit.Persistence.EFCore;
using KoalaKit.Persistence.EFCore.Oracle;
using KoalaKit.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoalaKit.Persistence.EfCore.Test.Tests
{
    [TestClass]
    public class OracleTests : TestContainerBase
    {
        private IDbContextFactory<KoalaDbContext>? contextFactory;
        private IStore<TestModelChild>? childrenStore;

        [TestMethod]
        //TODO: use Migration task
        public void Oracle_MigrateDb()
        {
            if (contextFactory is null)
                Assert.Fail("persistence Oracle module not well registered. IDbContextFactory is null");

            using var dbContext = contextFactory.CreateDbContext();

            dbContext.Database.EnsureCreated();
            Assert.IsTrue(true, "Oracle database schema created");
        }

        [TestMethod]
        public void Oracle_InsertRecord()
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
            Assert.IsTrue(true, "Oracle, Items added successfully!");
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddKoalaKit(builder => builder.AddModules(typeof(OracleModule)));
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