using System.Text.RegularExpressions;
using Core.Exceptions;

namespace Core.Validation
{
    public static class CityNameValidator
    {
        private static readonly Regex ValidCityNameRegex = new(@"^[a-zA-Z\u0590-\u05FF\s-]+$");

        public static void Validate(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                throw new ValidationException("City name cannot be empty.");

            cityName = cityName.Trim();

            if (cityName.Length < 2 || cityName.Length > 50)
                throw new ValidationException("City name must be between 2 and 50 characters.");

            if (!ValidCityNameRegex.IsMatch(cityName))
                throw new ValidationException("City name contains invalid characters.");
        }
    }
}
