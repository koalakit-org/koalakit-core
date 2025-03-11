using System.Reflection;
using KoalaKit.EventBus.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KoalaKit.EventBus.Extensions;

public static class HostExtensions
{
    public static IHost RunEventBusConsumers(this IHost host, params Type[] types)
    {
        return host.RunEventBusConsumers(types.Select((t) => t.GetTypeInfo().Assembly).ToArray()).GetAwaiter().GetResult();
    }

    public static async Task<IHost> RunEventBusConsumers(this IHost host, params Assembly[] assemblies)
    {
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var types = assemblies
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Where(t => !t.IsAbstract && typeof(IKoalaEventMessage).IsAssignableFrom(t));

        foreach (var type in types)
        {
            var consumerType = typeof(IEventBusConsumer<>).MakeGenericType(type);
            var consumer = serviceProvider.GetService(consumerType);

            if (consumer is not null)
            {
                var consumeMethod = consumerType.GetMethod(nameof(IEventBusConsumer<IKoalaEventMessage>.Consume));
                if (consumeMethod != null)
                {
                    var task = (Task?)consumeMethod.Invoke(consumer, [new CancellationToken()]);

                    if (task is not null)
                    {
                        await task.ConfigureAwait(false);
                    }
                }
            }

        }
        return host;
    }
}