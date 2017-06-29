namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public class Clouds
    {
        [JsonProperty("all")]
        public int AllPercent { get; set; }

    }
}