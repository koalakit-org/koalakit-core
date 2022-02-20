using System;
using KoalaKit.Extensions;
using KoalaKit.Messaging.Queuing;
using KoalaKit.Messaging.Test.Models;
using KoalaKit.Queuing.RabbitMq;
using KoalaKit.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoalaKit.Messaging.Test.Queuing
{
    [TestClass]
    public class RabbitMqTests : TestContainerBase
    {
        private IMessageQueuingPublisher<TestQueuingMessage>? publisher;

        [TestMethod]
        public void Publish()
        {
            if (publisher is null) Assert.Fail("messaging RabbitMQ module not registered. IMessageQueuingPublisher is null");

            publisher.Publish(new TestQueuingMessage { PublishingTimeUtc = DateTime.UtcNow});
            Assert.IsTrue(true, "message published successfully");
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddKoalaKitCore(builder => builder.AddModules(typeof(RabbitMqModule)));
            base.ConfigureServices(services);
            services.AddTransient<IMessagingHandler<TestQueuingMessage>, TestQueuingMessageHandler>();
        }
        protected override void ResolveServices()
        {
            base.ResolveServices();
            publisher = ServiceProvider.GetService<IMessageQueuingPublisher<TestQueuingMessage>>();
        }
    }
}