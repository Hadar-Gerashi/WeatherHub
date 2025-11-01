using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHub.Shared.DTOs;

namespace Core.Interfaces.External
{
    public interface IWeatherCacheService
    {
        Task<WeatherDataDto?> GetAsync(string cityName);
        Task SetAsync(string cityName, WeatherDataDto data);
    }
}
