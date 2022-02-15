using KoalaKit.Serializations;

namespace KoalaKit.Messaging
{
    public class MessageQueuingSerializer<TMessage> : KoalaJsonSerializer<TMessage> where TMessage : IQueuingMessage, new()
    {
        public override TMessage? Deserialize(byte[] bytes) => base.Deserialize(bytes);
        public override byte[] Serialize(TMessage message) => base.Serialize(message);
    }
}
