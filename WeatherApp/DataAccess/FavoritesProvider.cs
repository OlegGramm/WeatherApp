namespace WeatherApp.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Documents;
    using Helpers;
    using Models;
    using Newtonsoft.Json;

    public class FavoritesProvider
    {
        private const string FavoritesSettingsKey = "FavoriteCities";
        private const string IsFirstRunInitializedSettingsKey = "IsFirstRunInitialized";

        private static readonly Lazy<FavoritesProvider> instance = new Lazy<FavoritesProvider>(() => new FavoritesProvider(), true);

        public static FavoritesProvider Instance => instance.Value;

        private readonly List<City> favoriteCities;

        private FavoritesProvider()
        {
            if (!LocalSettingsProvider.GetValue<bool>(IsFirstRunInitializedSettingsKey))
            {
                var json = File.ReadAllText("cities.json");
                this.favoriteCities = JsonConvert.DeserializeObject<List<City>>(json);
                this.SaveState();
                LocalSettingsProvider.SetValue(IsFirstRunInitializedSettingsKey, true);
            }
            else
            {
                this.favoriteCities = LocalSettingsProvider.GetValue<List<City>>(FavoritesSettingsKey) ?? new List<City>();
            }
        }

        public List<City> GetCities()
        {
            return this.favoriteCities.ToList();
        }

        public void AddCity(City city)
        {
            this.favoriteCities.Add(city);
            this.SaveState();
        }

        public void RemoveCity(City city)
        {
            this.favoriteCities.RemoveAll(item => item.Id == city.Id);
            this.SaveState();
        }

        private void SaveState()
        {
            LocalSettingsProvider.SetValue(FavoritesSettingsKey, this.favoriteCities);
        }
    }
}