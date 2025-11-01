using Core.Entities;
using Core.Interfaces.External;
using Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using WeatherHub.Shared.DTOs;

public class WeatherApiClient : BaseOpenWeatherClient, IWeatherApiClient
{
    public WeatherApiClient(HttpClient httpClient, IConfiguration configuration)
        : base(httpClient, configuration) { }

    public async Task<WeatherDataDto?> GetWeatherAsync(Coordinates coordinates)
    {
        var doc = await GetJsonAsync(
            $"/data/2.5/forecast?lat={coordinates.Latitude}&lon={coordinates.Longitude}"
        );
 

        var forecasts = new List<HourlyForecastDto>();
        foreach (var item in doc.RootElement.GetProperty("list").EnumerateArray())
        {
            forecasts.Add(new HourlyForecastDto
            {
                ForecastTime = DateTimeOffset
                    .FromUnixTimeSeconds(item.GetProperty("dt").GetInt64()).UtcDateTime,
                Temperature = item.GetProperty("main").GetProperty("temp").GetDouble(),
                Condition = item.GetProperty("weather")[0].GetProperty("description").GetString()
            });
        }

        return new WeatherDataDto
        {
           
            HourlyForecasts = forecasts,
            RetrievedAt = DateTime.UtcNow
        };
    }
}
