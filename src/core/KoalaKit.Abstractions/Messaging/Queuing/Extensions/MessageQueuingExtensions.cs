using KoalaKit.Options;
using KoalaKit.Serializations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Messaging.Queuing
{
    public static class MessageQueuingExtensions
    {
        public static KoalaOptionsBuilder AddMessageQueuingCore(this KoalaOptionsBuilder koala)
        {
            if(koala.Configuration == null )
                throw new ArgumentNullException();

            koala.Services.Configure<MessageQueuingOptions>(options => koala.Configuration.GetSection(nameof(MessageQueuingOptions)).Bind(options));
            koala.Services.AddScoped(typeof(ISerializer<>), typeof(MessagingSerializer<>));
            koala.Services.AddScoped(typeof(IMessageQueueFactory<>), typeof(DefaultMessageQueueFactory<>));
            koala.Services.AddScoped(typeof(IMessageQueuingConnectionSelector<>), typeof(DefaultMessageQueuingConnectionSelector<>));
            return koala;
        }
    }
}
