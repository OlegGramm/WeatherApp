namespace WeatherApp.Helpers
{
    using System.IO;
    using Newtonsoft.Json;

    public static class LocalSettingsProvider
    {
        public static void SetValue(string key, object value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            File.WriteAllText(key, serializedValue);
        }

        public static T GetValue<T>(string key)
        {
            try
            {
                var value = File.ReadAllText(key);
                return !string.IsNullOrEmpty(value) ? JsonConvert.DeserializeObject<T>(value) : default(T);
            }
            catch (FileNotFoundException)
            {
                return default(T);
            }
        }
    }
}