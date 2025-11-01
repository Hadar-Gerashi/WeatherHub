using System.Text.RegularExpressions;

namespace WeatherHubClient.Validation
{
    public static class CityNameValidator
    {
        private static readonly Regex validCityRegex = new(@"^[a-zA-Z\u0590-\u05FF\s-]+$");

        public static string? Validate(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return "Please enter a city.";

            city = city.Trim();

            if (city.Length < 2 || city.Length > 50)
                return "City name must be between 2 and 50 characters.";

            if (!validCityRegex.IsMatch(city))
                return "City name contains invalid characters.";

            return null; 
        }
    }
}
