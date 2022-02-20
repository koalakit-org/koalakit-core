using System;
using System.Threading.Tasks;
using KoalaKit.Messaging.Bus;

namespace KoalaKit.Messaging.Test.Models
{
    public class TestBusMessage : IBusMessage
    {
        public DateTime PublishingTimeUtc { get; set; }
    }
    public class TestBusMessageHandler : IMessagingHandler<TestBusMessage>
    {
        public async Task<bool> HandleAsync(TestBusMessage message)
        {
            //TODO: implement
            return true;
        }
    }
}
