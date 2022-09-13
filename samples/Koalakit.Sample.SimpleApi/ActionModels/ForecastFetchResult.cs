namespace Koalakit.Sample.SimpleApi.ActionModels
{
    public class ForecastFetchResult
    {
        public ForecastFetchResult(Guid id, DateTime date, int temperatureC, int temperatureF)
        {
            Id = id;
            Date=date;
            TemperatureC=temperatureC;
            TemperatureF=temperatureF;
        }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
    }
}
