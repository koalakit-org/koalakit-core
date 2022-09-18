using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace KoalaKit.Messaging.Queuing.Extensions
{
    public static class HostExtensions
    {
        public static IHost RunQueuingConsumers(this IHost host, params Type[] types)
        {
            return host.RunQueuingConsumers(types.Select((Type t) => t.GetTypeInfo().Assembly).ToArray());
        }

        public static IHost RunQueuingConsumers(this IHost host, params Assembly[] assemblies)
        {
            var types = assemblies
                .SelectMany(assembly => assembly.GetExportedTypes())
                .Where(t => !t.IsAbstract && typeof(IQueuingMessage).IsAssignableFrom(t));

            foreach (var type in types)
            {
                var consmer = host.Services.GetService(typeof(IMessageQueuingConsumer<>).MakeGenericType(type));
                consmer?.GetType().GetMethod("Consume")?.Invoke(consmer,null);
            }
            return host;
        }
    }
}
