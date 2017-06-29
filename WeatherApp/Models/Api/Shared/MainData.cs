namespace WeatherApp.Models
{
    using Newtonsoft.Json;

    public class MainData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public double HumidityPercent { get; set; }

        [JsonProperty("temp_min")]
        public double TemperatureMinimum { get; set; }

        [JsonProperty("temp_max")]
        public double TemperatureMaximum { get; set; }

        [JsonProperty("sea_level")]
        public double PressureSeaLevel { get; set; }

        [JsonProperty("grnd_level")]
        public double PressureGroundLevel { get; set; }

        [JsonProperty("temp_kf")]
        public double TempKf { get; set; }

    }
}