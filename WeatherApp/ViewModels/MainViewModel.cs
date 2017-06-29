namespace WeatherApp.ViewModels
{
    using DataAccess;

    public class MainViewModel : ViewModel
    {
        private string searchQuery;

        public MainViewModel()
        {
            this.Initialize();
        }

        public string SearchQuery
        {
            get => this.searchQuery;
            set
            {
                this.searchQuery = value;
                base.RaisePropertyChanged();
            }
        }
        
        private async void Initialize()
        {
            this.BusyCount++;
            
            var ara = await OpenWeatherMapServiceProvider.Instance.GetCurrentWeather("London");
            var ara1 = await OpenWeatherMapServiceProvider.Instance.GetThreeHourForecast("London");
            var ara2 = await OpenWeatherMapServiceProvider.Instance.GetDailyForecast("London");

            this.BusyCount--;
        }
    }
}