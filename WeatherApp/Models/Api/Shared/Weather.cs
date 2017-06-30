namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public class Weather
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonIgnore]
        public string IconPath => $"http://openweathermap.org/img/w/{Icon}.png";

    }
}