namespace WeatherApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using DataAccess;
    using GalaSoft.MvvmLight.CommandWpf;
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
            this.QuerySubmittedCommand = new RelayCommand(this.QuerySubmittedExecute);
            this.AddToFavoritesCommand = new RelayCommand(this.AddToFavoritesExecute, () => 
            this.FavoriteCities.All(item => item.Id != this.CurrentWeatherData?.Id));
            this.RemoveFromFavoritesCommand = new RelayCommand(this.RemoveFromFavoritesExecute, () => this.FavoriteCities.Any(item => item.Id == this.CurrentWeatherData?.Id));

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

                if (this.selecterDailyWeatherData != null)
                {
                    this.UpdateHourlyData();
                }
            }
        }

        public ObservableCollection<ThreeHourWeatherData> HourlyData { get; set; }

        public ObservableCollection<City> FavoriteCities { get; set; }

        public RelayCommand QuerySubmittedCommand { get; }

        public RelayCommand AddToFavoritesCommand { get; }

        public RelayCommand RemoveFromFavoritesCommand { get; }

        private void Initialize()
        {
            this.FavoriteCities = new ObservableCollection<City>(FavoritesProvider.Instance.GetCities());
            if (this.FavoriteCities.Any())
            {
                this.SetCityData(this.FavoriteCities.FirstOrDefault().Name);
            }
        }

        private void QuerySubmittedExecute()
        {
            if (!string.IsNullOrWhiteSpace(this.SearchQuery))
            {
                this.SetCityData(this.SearchQuery);
            }
        }

        private async void SetCityData(string city)
        {
            this.BusyCount++;

            this.CurrentWeatherData = await OpenWeatherMapServiceProvider.Instance.GetCurrentWeather(city);
            this.ThreeHourForecast = await OpenWeatherMapServiceProvider.Instance.GetThreeHourForecast(city);
            this.DailyForecast = await OpenWeatherMapServiceProvider.Instance.GetDailyForecast(city);

            this.SelectedDailyWeatherData = this.DailyForecast.Items.FirstOrDefault();
            this.RaiseFavoriteCanExecuteCommands();

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

        private void AddToFavoritesExecute()
        {
            this.FavoriteCities.Add(this.ThreeHourForecast.City);
            FavoritesProvider.Instance.AddCity(this.ThreeHourForecast.City);
            this.RaiseFavoriteCanExecuteCommands();
        }

        private void RemoveFromFavoritesExecute()
        {
            var city = this.FavoriteCities.FirstOrDefault(item => item.Id == this.CurrentWeatherData.Id);
            this.FavoriteCities.Remove(city);
            FavoritesProvider.Instance.RemoveCity(city);
            this.RaiseFavoriteCanExecuteCommands();
        }

        private void RaiseFavoriteCanExecuteCommands()
        {
            this.AddToFavoritesCommand.RaiseCanExecuteChanged();
            this.RemoveFromFavoritesCommand.RaiseCanExecuteChanged();
        }
    }
}