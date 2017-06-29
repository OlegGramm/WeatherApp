namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public abstract class DetailedWeatherData : WeatherData
    {
        [JsonProperty("main")]
        public MainData MainData { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("rain")]
        public Precipitation Rain { get; set; }

        [JsonProperty("snow")]
        public Precipitation Snow { get; set; }
    }
}