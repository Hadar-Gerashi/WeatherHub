using Core.Entities;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services;
using Utils;
using Core.Interfaces.Services;

namespace WeatherHubServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityCoordinateController : ControllerBase
    {
        private readonly ICityCoordinateService cityService;

        public CityCoordinateController(ICityCoordinateService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CityCoordinateDto>>>> GetAllCities()
        {
            var cities = await cityService.GetAllCitiesAsync();
            return Ok(ApiResponse<List<CityCoordinateDto>>.Ok(cities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CityCoordinateDto>>> GetCityById(int id)
        {
            var city = await cityService.GetCityByIdAsync(id);
            if (city == null)
                return NotFound(ApiResponse<CityCoordinateDto>.Fail("City not found"));

            return Ok(ApiResponse<CityCoordinateDto>.Ok(city));
        }

        [HttpGet("by-name")]
        public async Task<ActionResult<ApiResponse<CityCoordinateDto>>> GetCityByName([FromQuery] string name)
        {
            var city = await cityService.GetCityByNameAsync(name);

            return Ok(ApiResponse<CityCoordinateDto>.Ok(city!));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CityCoordinateDto>>> AddCity([FromBody] AddCityRequest city)
        {
            var addedCity = await cityService.AddCityAsync(city.Name);


            return Ok(ApiResponse<CityCoordinateDto>.Ok(addedCity!));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var deleted = await cityService.DeleteCityAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail("City not found"));

            return Ok(ApiResponse<string>.Ok("City deleted successfully"));
        }


        [HttpPut("{id}/last-searched")]
        public async Task<IActionResult> UpdateLastSearched(int id)
        {
            var updated = await cityService.UpdateLastSearchedAsync(id);

            if (!updated)
                return NotFound(ApiResponse<string>.Fail("City not found"));

            return Ok(ApiResponse<string>.Ok("City updated successfully"));
        }

    }


}
