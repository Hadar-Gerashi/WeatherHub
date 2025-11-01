using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHub.Shared.DTOs;

namespace Core.Interfaces.External
{
    public interface IWeatherApiClient
    {
        Task<WeatherDataDto?> GetWeatherAsync(Coordinates coordinates);
    }
}
