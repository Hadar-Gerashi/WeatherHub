using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHub.Shared.DTOs;

namespace Core.Interfaces.Services
{
    public interface IWeatherService
    {
        Task<WeatherDataDto?> GetWeatherAsync(string cityName);
    }
}
