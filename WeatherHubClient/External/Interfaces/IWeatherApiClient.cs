using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHub.Shared.DTOs;
using WeatherHubClient.Models;

namespace WeatherHubClient.External.Interfaces
{
    public interface IWeatherApiClient
    {
        Task<WeatherDataDto> GetWeatherAsync(string cityName);
    }

}
