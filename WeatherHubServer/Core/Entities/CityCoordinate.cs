using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    
    public class CityCoordinate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Coordinates? Coordinates { get; set; }
        public DateTime LastSearched { get; set; } = DateTime.UtcNow;
    }
}
