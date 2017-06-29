namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public class DailyWeatherData : WeatherData
    {
        [JsonProperty("temp")]
        public Temperature Temperature { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public int HumidityPercent { get; set; }

        [JsonProperty("speed")]
        public double WindSpeed { get; set; }
        
        [JsonProperty("deg")]
        public int Degrees { get; set; }

        [JsonProperty("clouds")]
        public int CloudsPercent { get; set; }
    }
}