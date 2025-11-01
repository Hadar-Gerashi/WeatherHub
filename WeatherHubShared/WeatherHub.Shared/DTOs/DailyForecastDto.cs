using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherHub.Shared.DTOs
{
    public class DailyForecastDto
    {
        public DateTime Date { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; } 
        public string? Condition { get; set; }


    }
}
