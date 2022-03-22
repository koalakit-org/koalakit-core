using System;
using System.Threading.Tasks;
using KoalaKit.Caching.Memory;
using KoalaKit.Extensions;
using KoalaKit.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoalaKit.Caching.Test.Tests
{
    [TestClass]
    public class MemoryCacheTests : TestContainerBase
    {
        private ICache? cache;
        [TestMethod]
        public async Task TestAnyThing()
        {
            if (cache == null) Assert.Fail("Cache not implemented");

            var key = Guid.NewGuid().ToString();
            await cache.SetAsync(key, new TestCacheModel { TimeStamp = DateTime.UtcNow });
            await Task.Delay(500);
            var cachedItem = await cache.GetAsync<TestCacheModel>(key);
            Assert.IsFalse(cachedItem == null, "Cached item is null");
            Assert.IsTrue(DateTime.UtcNow.Subtract(cachedItem.TimeStamp).TotalMilliseconds < 1000);
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddKoalaKitCore(Configuration, builder => builder.AddModules(typeof(KoalaMemoryCacheModule)));
            base.ConfigureServices(services);
        }
        protected override void ResolveServices()
        {
            base.ResolveServices();
            cache = ServiceProvider.GetService<ICache>();
        }
    }
}