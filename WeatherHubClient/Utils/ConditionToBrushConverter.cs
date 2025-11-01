using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WeatherHubClient.Utils
{
    public class ConditionToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string condition = value?.ToString()?.ToLower() ?? "";

            return condition switch
            {
                var c when c.Contains("sun") => Brushes.Orange,
                var c when c.Contains("clear") => Brushes.Gold,
                var c when c.Contains("cloud") => Brushes.LightGray,
                var c when c.Contains("rain") => Brushes.SkyBlue,
                var c when c.Contains("snow") => Brushes.White,
                var c when c.Contains("storm") => Brushes.MediumPurple,
                var c when c.Contains("לא שוהה") => Brushes.Red,
                _ => Brushes.Gray
            };
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

