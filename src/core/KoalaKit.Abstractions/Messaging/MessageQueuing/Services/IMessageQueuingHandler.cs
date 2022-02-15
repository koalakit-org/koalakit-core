namespace KoalaKit.Messaging
{
    public interface IMessageQueuingHandler<in TMessage> where TMessage : IQueuingMessage, new()
    {
        Task<bool> HandleAsync(TMessage message);
    }
}
