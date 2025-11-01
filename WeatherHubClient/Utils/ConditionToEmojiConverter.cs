using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherHubClient.Utils
{
    public class ConditionToEmojiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string condition = value?.ToString()?.ToLower() ?? "";

            return condition switch
            {
                var c when c.Contains("sun") => "☀️",
                var c when c.Contains("clear") => "☀️",
                var c when c.Contains("cloud") => "☁️",
                var c when c.Contains("rain") => "🌧️",
                var c when c.Contains("snow") => "❄️",
                var c when c.Contains("storm") => "⛈️",
                var c when c.Contains("לא שוהה") => "🚫",
                _ => "❓"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
