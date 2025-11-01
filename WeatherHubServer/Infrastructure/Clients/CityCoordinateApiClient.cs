using Core.Entities;
using Core.Interfaces.Infrastructure;
using Infrastructure.Clients;
using Infrastructure.Clients.Responses;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

public class CityCoordinateApiClient : BaseOpenWeatherClient, ICityCoordinateApiClient
{
    public CityCoordinateApiClient(HttpClient httpClient, IConfiguration configuration)
        : base(httpClient, configuration) { }

    public async Task<CityCoordinate?> GetCoordinateAsync(string cityName)
    {
        if (string.IsNullOrWhiteSpace(cityName))
            return null;

        var doc = await GetJsonAsync($"/geo/1.0/direct?q={cityName}");


        var dataList = JsonSerializer.Deserialize<List<GeoApiResponse>>(
            doc.RootElement.GetRawText(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        var data = dataList?.FirstOrDefault();
        if (data == null) return null;

        return new CityCoordinate
        {
            Name = data.Name,
            Coordinates = new Coordinates
            {
                Latitude = data.Lat,
                Longitude = data.Lon
            },
            LastSearched = DateTime.UtcNow
        };
    }
}
