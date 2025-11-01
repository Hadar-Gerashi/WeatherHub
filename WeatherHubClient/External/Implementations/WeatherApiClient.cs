using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherHub.Shared.DTOs;
using WeatherHubClient.External.Interfaces;

namespace WeatherHubClient.External.Implementations
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly HttpClient httpClient;

        public WeatherApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public class ApiResponse<T>
        {
            public T? Data { get; set; }
        }


        public async Task<WeatherDataDto> GetWeatherAsync(string cityName)
        {
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];
            var url = $"api/Weather?city={cityName}";


            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-API-KEY", apiKey);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<WeatherDataDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Data ?? throw new Exception("No data in server response.");
        }




    }

}
