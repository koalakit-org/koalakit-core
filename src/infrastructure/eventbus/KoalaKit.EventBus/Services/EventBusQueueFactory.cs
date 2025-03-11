using KoalaKit.EventBus.Models;
using KoalaKit.Primitives.Extensions;

namespace KoalaKit.EventBus.Services;

public sealed class EventBusQueueFactory<TEto>(IEventBusConfigurationSelector<TEto> configurationSelector) : IEventBusQueueFactory<TEto> where TEto : IKoalaEventMessage
{
    private const string DefaultQueueName = "default";
    private readonly IEventBusConfigurationSelector<TEto> _configurationSelector = configurationSelector;

    public IEnumerable<EventBusQueueDefinition> Create()
    {
        var instance = Activator.CreateInstance<TEto>() as IKoalaEventMessage;
        var queueName = instance?.QueueName ?? DefaultQueueName;
        var consumerDefinition = _configurationSelector.SelectConsumer(queueName);
        if (consumerDefinition == null || !consumerDefinition.Active)
        {
            return [];
        }

        var quesues = new List<EventBusQueueDefinition>();
        if (consumerDefinition.Channels == null || !consumerDefinition.Channels.HasItems())
        {
            var connection = _configurationSelector.Select(queueName);
            quesues.Add(new EventBusQueueDefinition(queueName, $"{queueName}.*", connection));
            return quesues;
        }

        foreach (var channel in consumerDefinition.Channels)
        {
            var name = $"{queueName}.{channel}";
            var route = $"{queueName}.{channel}.*";
            var connection = _configurationSelector.Select(queueName);

            quesues.Add(new EventBusQueueDefinition(name, route, connection));
        }

        return quesues;
    }

    public EventBusQueueDefinition Create(TEto eto)
    {
        var queueName = eto.QueueName;
        return new EventBusQueueDefinition(queueName, $"{queueName}.*", _configurationSelector.Select(queueName));
    }
}