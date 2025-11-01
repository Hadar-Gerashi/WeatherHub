using Core.Entities;
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ICityCoordinateService
    {

        Task<CityCoordinateDto?> GetCityByNameAsync(string name);
        Task<CityCoordinateDto?> GetCityByIdAsync(int id);
        Task<List<CityCoordinateDto>> GetAllCitiesAsync();
        Task<bool> UpdateLastSearchedAsync(int cityId);
        Task<CityCoordinateDto?> AddCityAsync(string cityName);
        Task<bool> DeleteCityAsync(int id);

    }
}
