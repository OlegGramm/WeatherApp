namespace WeatherApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using DataAccess;
    using Models;

    public class MainViewModel : ViewModel
    {
        private string searchQuery;
        private CurrentWeatherData currentWeatherData;
        private ThreeHourForecast threeHourForecast;
        private DailyForecast dailyForecast;

        private DailyWeatherData selecterDailyWeatherData;

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

        public CurrentWeatherData CurrentWeatherData
        {
            get => this.currentWeatherData;
            set
            {
                this.currentWeatherData = value;
                base.RaisePropertyChanged();
            }
        }

        public ThreeHourForecast ThreeHourForecast
        {
            get => this.threeHourForecast;
            set
            {
                this.threeHourForecast = value;
                base.RaisePropertyChanged();
            }
        }

        public DailyForecast DailyForecast
        {
            get => this.dailyForecast;
            set
            {
                this.dailyForecast = value;
                base.RaisePropertyChanged();
            }
        }

        public DailyWeatherData SelectedDailyWeatherData
        {
            get => this.selecterDailyWeatherData;
            set
            {
                this.selecterDailyWeatherData = value;
                base.RaisePropertyChanged();
                this.UpdateHourlyData();
            }
        }

        public ObservableCollection<ThreeHourWeatherData> HourlyData { get; set; }
        
        private async void Initialize()
        {
            this.BusyCount++;

            this.CurrentWeatherData = await OpenWeatherMapServiceProvider.Instance.GetCurrentWeather("Kharkiv");
            this.ThreeHourForecast = await OpenWeatherMapServiceProvider.Instance.GetThreeHourForecast("Kharkiv");
            this.DailyForecast = await OpenWeatherMapServiceProvider.Instance.GetDailyForecast("Kharkiv");

            this.SelectedDailyWeatherData = this.DailyForecast.Items.FirstOrDefault();

            this.BusyCount--;
        }

        private void UpdateHourlyData()
        {
            var firstItem = this.ThreeHourForecast.Items.FirstOrDefault(item => item.CalculationLocalTime.Date == this.SelectedDailyWeatherData.CalculationTime.Date);
            var index = firstItem != null ? this.ThreeHourForecast.Items.IndexOf(firstItem) : 0;
            var items = this.ThreeHourForecast.Items.GetRange(index, 8);
            this.HourlyData = new ObservableCollection<ThreeHourWeatherData>(items);
            base.RaisePropertyChanged(() => this.HourlyData);
        }
    }
}