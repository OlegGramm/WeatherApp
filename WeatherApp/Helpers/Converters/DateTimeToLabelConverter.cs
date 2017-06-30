namespace WeatherApp.Helpers.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DateTimeToLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var format = parameter?.ToString();
                return ((DateTime)value).ToString(format, CultureInfo.InvariantCulture);
            }
            catch (InvalidCastException)
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}