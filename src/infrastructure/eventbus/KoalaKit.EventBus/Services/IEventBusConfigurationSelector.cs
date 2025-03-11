using KoalaKit.EventBus.Models;

namespace KoalaKit.EventBus.Services;

public interface IEventBusConfigurationSelector<TEto> where TEto : IKoalaEventMessage
{
    EventBusConnectionDefinition Select();
    EventBusConnectionDefinition Select(string identifier);
    EventBusConsumerDefinition? SelectConsumer(string identifier);
}