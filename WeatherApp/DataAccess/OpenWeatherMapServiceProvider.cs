namespace WeatherApp.DataAccess
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;

    public class OpenWeatherMapServiceProvider
    {
        private const string ApiKey = "2f63b951fbd17efb48af5e2283f48f70";
        private const string ApiUri = "http://api.openweathermap.org/data/2.5/";

        private static readonly string CurrentWeatherRequestUri = $"{ApiUri}weather";
        private static readonly string ThreeHourForecastRequestUri = $"{ApiUri}forecast";
        private static readonly string DailyForecastRequestUri = $"{ApiUri}forecast/daily";

        private static readonly string RequiredParameters = $"&units=metric&appid={ApiKey}";

        private static readonly Lazy<OpenWeatherMapServiceProvider> instance = new Lazy<OpenWeatherMapServiceProvider>(() => new OpenWeatherMapServiceProvider(), true);

        private OpenWeatherMapServiceProvider() { }

        public static OpenWeatherMapServiceProvider Instance => instance.Value;

        public async Task<CurrentWeatherData> GetCurrentWeather(string city) => await GetRequest<CurrentWeatherData>($"{CurrentWeatherRequestUri}?q={city}{RequiredParameters}");

        public async Task<ThreeHourForecast> GetThreeHourForecast(string city) => await GetRequest<ThreeHourForecast>($"{ThreeHourForecastRequestUri}?q={city}{RequiredParameters}");

        public async Task<DailyForecast> GetDailyForecast(string city)
        {
            var result = await GetRequest<DailyForecast>($"{DailyForecastRequestUri}?q={city}&cnt=6{RequiredParameters}");

            //Sometimes includes yesterday
            result.Items?.RemoveAll(item => item.CalculationTime.Date < DateTime.Today);
            if (result.Items?.Count > 5)
            {
                result.Items = result.Items.Take(5).ToList();
            }

            return result;
        }

        private static async Task<T> GetRequest<T>(string request) where T : class
        {
            var client = new HttpClient();
            var response = await client.GetAsync(request);

            if (response != null)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(json);
            }

            return null;
        }
    }
}