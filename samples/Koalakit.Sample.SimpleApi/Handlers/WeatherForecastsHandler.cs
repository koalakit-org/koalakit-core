using Koalakit.Sample.SimpleApi.ActionModels;
using Koalakit.Sample.SimpleApi.Services;
using KoalaKit.Messaging;

namespace Koalakit.Sample.SimpleApi.Handlers
{
    public class WeatherForecastsHandler : IMessagingHandler<WeatherForecastsPublishingParameters>
    {
        private readonly ILogger<WeatherForecastsHandler> logger;
        private readonly WeatherForecastsService service;

        public WeatherForecastsHandler(ILoggerFactory logger, WeatherForecastsService service)
        {
            this.logger = logger.CreateLogger<WeatherForecastsHandler>();
            this.service = service;
        }

        public async Task<bool> HandleAsync(WeatherForecastsPublishingParameters message)
        {
            logger.LogDebug("Forecasts create handler, temp: {temp}. date: {date}", message.Temp, message.Date);
            await service.Create(new ForecastCreateParameters
            {
                Date = message.Date,
                Temp = message.Temp,
            });

            return true;
        }
    }
}
