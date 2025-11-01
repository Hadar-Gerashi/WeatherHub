using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Infrastructure;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Validation;
using Data;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class CityCoordinateService : ICityCoordinateService
    {

        private readonly ICityCoordinateRepository cityRepository;
        private readonly ICityCoordinateApiClient apiClient;
        private readonly IMapper mapper;

        public CityCoordinateService(ICityCoordinateRepository cityRepository, ICityCoordinateApiClient apiClient, IMapper mapper)
        {
            this.cityRepository = cityRepository;
            this.apiClient = apiClient;
            this.mapper = mapper;
        }

        public async Task<CityCoordinateDto?> AddCityAsync(string cityName)
        {
         
            CityNameValidator.Validate(cityName);


            var existingCity = await cityRepository.GetByNameAsync(cityName);

            if (existingCity != null)
            {
                existingCity.LastSearched = DateTime.UtcNow;
                await cityRepository.UpdateAsync(existingCity);
                return mapper.Map<CityCoordinateDto>(existingCity);
            }

            var newCity = await apiClient.GetCoordinateAsync(cityName);
            if (newCity == null)
            {
                return null;
            }

            newCity.LastSearched = DateTime.UtcNow;
            await cityRepository.AddAsync(newCity);
            return mapper.Map<CityCoordinateDto>(newCity);
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            var city = await cityRepository.GetByIdAsync(id);
            if (city == null) return false;
            await cityRepository.DeleteAsync(id);
            return true;
        }



        public async Task<List<CityCoordinateDto>> GetAllCitiesAsync()
        {
            var cities = await cityRepository.GetAllAsync();
            return mapper.Map<List<CityCoordinateDto>>(cities);
        }

        public async Task<CityCoordinateDto?> GetCityByIdAsync(int id)
        {
            var city = await cityRepository.GetByIdAsync(id);
            return mapper.Map<CityCoordinateDto>(city);
        }

        public async Task<CityCoordinateDto?> GetCityByNameAsync(string name)
        {
         
            CityNameValidator.Validate(name);

            var city = await cityRepository.GetByNameAsync(name.ToLower());
            return mapper.Map<CityCoordinateDto>(city);
        }

        public async Task<bool> UpdateLastSearchedAsync(int cityId)
        {
            var city = await cityRepository.GetByIdAsync(cityId);
            if (city == null) return false;

            city.LastSearched = DateTime.UtcNow;
            await cityRepository.UpdateAsync(city);
            return true;
        }


    }
}
