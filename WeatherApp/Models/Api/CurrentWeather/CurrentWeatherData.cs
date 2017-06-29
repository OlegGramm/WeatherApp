namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public class CurrentWeatherData : DetailedWeatherData
    {
        [JsonProperty("coord")]
        public Coordinates Coordinated { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("sys")]
        public SystemData SystemData { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cod")]
        public int Cod { get; set; }
    }
}