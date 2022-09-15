using KoalaKit.Messaging.Queuing;

namespace Koalakit.Sample.SimpleApi.ActionModels
{
    public class WeatherForecastsPublishingParameters : IQueuingMessage
    {
        public string QueueName => "WeatherForecasts";
        public int Temp { get; set; }
        public DateTime Date { get; set; }
    }
}
