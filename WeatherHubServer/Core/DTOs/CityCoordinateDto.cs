using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CityCoordinateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Coordinates? Coordinates { get; set; }

    }
}
