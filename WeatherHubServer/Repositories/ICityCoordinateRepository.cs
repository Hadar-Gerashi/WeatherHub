using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICityCoordinateRepository
    {
        Task<CityCoordinate?> GetByIdAsync(int id);
        Task<CityCoordinate?> GetByNameAsync(string name);
        Task<List<CityCoordinate>> GetAllAsync();
        Task AddAsync(CityCoordinate city);
        Task UpdateAsync(CityCoordinate city);
        Task DeleteAsync(int id);
    }
}
