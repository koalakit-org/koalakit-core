using KoalaKit.EventBus.Models;
using KoalaKit.EventBus.Services;
using KoalaKit.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.EventBus.Extensions;

public static class KoalaContextExtensions
{
    public static KoalaContext AddKoalaEventsBusCore(this KoalaContext koala)
    {
        if (koala.Configuration == null)
        {
            throw new ArgumentException("Koala Configuration is null", nameof(koala));
        }

        koala.Services.Configure<EventBusSettings>(options => koala.Configuration.GetSection(nameof(EventBusSettings)).Bind(options));
        koala.Services.AddSingleton(typeof(IEventBusQueueFactory<>), typeof(EventBusQueueFactory<>));
        koala.Services.AddSingleton(typeof(IEventBusConfigurationSelector<>), typeof(EventBusConfigurationSelector<>));
        return koala;
    }
}