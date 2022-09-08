using KoalaKit.Persistence;

namespace Koalakit.Sample.SimpleApi.Entities
{
    public class Forecast : SampleForecastBaseEntity
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    }
}
