using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Clients.Responses
{
    public class GeoApiResponse
    {
        public string Name { get; set; } = default!;
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
