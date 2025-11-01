using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherHub.Shared.DTOs
{

    public class WeatherDataDto
    {
        public string? CityName { get; set; }

        public List<HourlyForecastDto>? HourlyForecasts { get; set; } = new();
        public List<DailyForecastDto> DailyForecasts { get; set; } = new();
        public DateTime? RetrievedAt { get; set; }
    }
}
