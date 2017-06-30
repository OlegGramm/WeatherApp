namespace WeatherApp.Helpers.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DoubleRoundingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = (double)value;
            return Math.Round(doubleValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}