namespace KoalaKit.Messaging.Bus
{
    public interface IBusMessageDefinitionFactory<in TMessage> where TMessage : class, IBusMessage
    {
        BusMessageDefinition Create();
        BusMessageDefinition Create(TMessage message);
    }
}
