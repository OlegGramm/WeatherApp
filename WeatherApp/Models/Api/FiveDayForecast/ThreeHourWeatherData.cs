namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public class ThreeHourWeatherData : DetailedWeatherData
    {
        [JsonProperty("dt_txt")]
        public string CalculationTimeLabel { get; set; }

    }
}