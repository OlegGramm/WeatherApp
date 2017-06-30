namespace WeatherApp.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public abstract class Forecast<T> where T : WeatherData
    {
        [JsonProperty("cod")]
        public int Cod { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }

        [JsonProperty("cnt")]
        public int Count { get; set; }

        [JsonProperty("list")]
        public List<T> Items { get; set; }
    }
}