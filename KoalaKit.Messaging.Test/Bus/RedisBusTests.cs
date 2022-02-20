using System;
using KoalaKit.Bus.Redis;
using KoalaKit.Extensions;
using KoalaKit.Messaging.Bus;
using KoalaKit.Messaging.Test.Models;
using KoalaKit.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoalaKit.Messaging.Test.Bus
{
    [TestClass]
    public class RedisBusTests : TestContainerBase
    {
        private IPubChannel<TestBusMessage>? publishChannel;

        [TestMethod]
        public void Redis_Publish()
        {
            if (publishChannel is null) Assert.Fail("messaging Redis module not registered. IPubChannel is null");

            publishChannel.PublishAsync(new TestBusMessage
            {
                PublishingTimeUtc = DateTime.Now
            });
            Assert.IsTrue(true, "message published successfully");
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddKoalaKitCore(builder => builder.AddModules(typeof(RedisMessagingBusModule)));
            base.ConfigureServices(services);
            services.AddTransient<IMessagingHandler<TestBusMessage>, TestBusMessageHandler>();
        }
        protected override void ResolveServices()
        {
            base.ResolveServices();
            publishChannel = ServiceProvider.GetService<IPubChannel<TestBusMessage>>();
        }
    }
}
