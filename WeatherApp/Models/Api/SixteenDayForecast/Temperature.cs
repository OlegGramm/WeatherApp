namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public class Temperature
    {
        [JsonProperty("day")]
        public double Day { get; set; }

        [JsonProperty("min")]
        public double Minimum { get; set; }

        [JsonProperty("max")]
        public double Maximum { get; set; }

        [JsonProperty("night")]
        public double Night { get; set; }

        [JsonProperty("eve")]
        public double Evening { get; set; }

        [JsonProperty("morn")]
        public double Morning { get; set; }

    }
}