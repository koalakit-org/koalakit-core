namespace KoalaKit.Messaging
{
    public interface IQueuingMessage : IKoalaMessage
    {
        string QueueName { get; }
    }
}
