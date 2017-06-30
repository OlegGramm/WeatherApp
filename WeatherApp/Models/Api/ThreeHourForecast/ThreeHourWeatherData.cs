namespace WeatherApp.Models
{
    using System;
    using Newtonsoft.Json;

    public class ThreeHourWeatherData : DetailedWeatherData
    {
        [JsonProperty("dt_txt")]
        public string CalculationTimeLabel { get; set; }

        [JsonIgnore]
        public DateTime CalculationLocalTime => DateTime.Parse(this.CalculationTimeLabel);

    }
}