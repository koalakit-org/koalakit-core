using KoalaKit.Extensions;
using KoalaKit.Persistence.EfCore.Test.Models;
using KoalaKit.Persistence.EFCore;
using KoalaKit.Persistence.EFCore.PostgreSql;
using KoalaKit.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoalaKit.Persistence.EfCore.Test.Tests
{
    [TestClass]
    public class PostgreSqlTests : TestContainerBase
    {
        private IDbContextFactory<KoalaDbContext>? contextFactory;
        private IStore<TestModelChild>? childrenStore;

        [TestMethod]
        public void PostgreSql_MigrateDb()
        {
            if (contextFactory is null) Assert.Fail("persistence PostgreSQL module not well registered. IDbContextFactory is null");

            using var dbContext = contextFactory.CreateDbContext();
            dbContext.Database.EnsureCreated();
            Assert.IsTrue(true, "PostgreSql database schema created");
        }

        [TestMethod]
        public void PostgreSql_InsertRecord()
        {
            if(childrenStore is null) Assert.Fail("Store registration failed");
            childrenStore?.AddAsync(new TestModelChild
            {
                Name = "Test Name",
                Parent = new TestModelParent
                {
                    Name = "Test Name",
                }
            }).Wait();
            Assert.IsTrue(true, "PostgreSQL, Items added successfully!");
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddKoalaKitCore(builder => builder.AddModules(typeof(PostgreSqlModule)));
            EntityProvidersCollection.AddDbEntityProvider(typeof(TestDbEntityProvider).Assembly);
            base.ConfigureServices(services);
        }
        protected override void ResolveServices()
        {
            base.ResolveServices();
            var scope = ServiceProvider.CreateScope();
            contextFactory = scope.ServiceProvider.GetService<IDbContextFactory<KoalaDbContext>>();
            childrenStore = scope.ServiceProvider.GetService<IStore<TestModelChild>>(); }
    }
}
