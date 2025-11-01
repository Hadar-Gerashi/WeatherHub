using Core.Interfaces.External;
using Core.Interfaces.Services;
using Core.Validation;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using WeatherHub.Shared.DTOs;

namespace Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly ICityCoordinateService cityService;
        private readonly IWeatherCacheService cache;
        private readonly IWeatherApiClient api;
  
        public WeatherService(
            ICityCoordinateService cityService,
            IWeatherCacheService cache,
            IWeatherApiClient api)
        {
            this.cityService = cityService;
            this.cache = cache;
            this.api = api;

        }

    

        public async Task<WeatherDataDto?> GetWeatherAsync(string cityName)
        {

            CityNameValidator.Validate(cityName);

            var cached = await cache.GetAsync(cityName);

            if (cached != null) {
                cached.RetrievedAt = DateTime.UtcNow;
                return cached;
            }
               

 
            var city = await cityService.GetCityByNameAsync(cityName);
            if (city == null)
            {
                city = await cityService.AddCityAsync(cityName);
                if (city == null) return null;
            }


            var weather = await api.GetWeatherAsync(city?.Coordinates!);
            if (weather != null)
            {
                weather.CityName = city!.Name;                      
                weather.DailyForecasts = SummarizeByDay(weather.HourlyForecasts!); 
                weather.RetrievedAt = DateTime.UtcNow;          
                await cache.SetAsync(cityName, weather);           
        
            }

            return weather;
        }

        private List<DailyForecastDto> SummarizeByDay(List<HourlyForecastDto> forecasts)
        {
            var today = DateTime.UtcNow.Date;

            return forecasts
                .GroupBy(f => f.ForecastTime.Date)
                .Where(g => g.Key >= today)       
                .OrderBy(g => g.Key)
                .Take(5)                         
                .Select(g => new DailyForecastDto
                {
                    Date = g.Key,
                    TempMin = g.Min(x => x.Temperature),
                    TempMax = g.Max(x => x.Temperature),
                    Condition = g
                        .GroupBy(x => x.Condition)
                        .OrderByDescending(x => x.Count())
                        .First().Key
                })
                .ToList();
        }



    }



}
