using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherHub.Shared.DTOs
{
    public class HourlyForecastDto
    {
        public DateTime ForecastTime { get; set; }
        public double Temperature { get; set; }
        public string? Condition { get; set; }

    }
}
