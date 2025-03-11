namespace KoalaKit.EventBus.Models;

public interface IKoalaEventMessage
{
    string QueueName { get; }
}