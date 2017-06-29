namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public class City
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public Coordinates Coordinated { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

    }
}