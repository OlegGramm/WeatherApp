namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public class Precipitation
    {
        [JsonProperty("3h")]
        public double ThreeHour { get; set; }
    }
}