using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Utils;
using WeatherHub.Shared.DTOs;

namespace WeatherHubServer.Controllers
{

    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string city)
        {

            var weather = await weatherService.GetWeatherAsync(city);

            if (weather == null)
                return NotFound(ApiResponse<string>.Fail("City not found or weather data unavailable."));

            return Ok(ApiResponse<WeatherDataDto>.Ok(weather));
        }

 

    }
}