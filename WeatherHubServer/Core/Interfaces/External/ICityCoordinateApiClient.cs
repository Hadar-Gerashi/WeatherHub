using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Infrastructure  
{
    public interface ICityCoordinateApiClient
    {
        Task<CityCoordinate?> GetCoordinateAsync(string cityName);
    }
}
