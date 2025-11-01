using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherHubClient.Models
{
    public class RecentCity : City
    {
        public DateTime? AddedAt { get; set; } = DateTime.Now;
    }
}
