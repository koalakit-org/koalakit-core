namespace Koalakit.Sample.SimpleApi.ActionModels
{
    public class ForecastFetchResult
    {
        public ForecastFetchResult(DateTime date, int temperatureC, int temperatureF)
        {
            Date=date;
            TemperatureC=temperatureC;
            TemperatureF=temperatureF;
        }

        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
    }
}
