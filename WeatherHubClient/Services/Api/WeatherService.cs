using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHub.Shared.DTOs;
using WeatherHubClient.External.Interfaces;

namespace WeatherHubClient.Services.Api
{
    public class WeatherService
    {
        private readonly IWeatherApiClient apiClient;

        public WeatherService(IWeatherApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<WeatherDataDto?> GetWeatherAsync(string cityName)
        {
            try
            {
                return await apiClient.GetWeatherAsync(cityName);
            }
            catch
            {
                return null;
            }
        }



    }

}
