using Core.Entities;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CityCoordinateRepository : ICityCoordinateRepository
    {
        private readonly AppDbContext context;

        public CityCoordinateRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<CityCoordinate?> GetByIdAsync(int id)
        {
            return await context.CitiesCoordinates.FindAsync(id);
        }

        public async Task<CityCoordinate?> GetByNameAsync(string name)
        {
            //return await context.CitiesCoordinates.FirstOrDefaultAsync(c => c.Name!.ToLower() == name.ToLower());
            return await context.CitiesCoordinates
    .FirstOrDefaultAsync(c => c.Name != null && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        }


        public async Task<List<CityCoordinate>> GetAllAsync()
        {
            return await context.CitiesCoordinates.ToListAsync();
        }


        public async Task AddAsync(CityCoordinate city)
        {
            context.CitiesCoordinates.Add(city);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CityCoordinate city)
        {
            context.CitiesCoordinates.Update(city);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var city = await context.CitiesCoordinates.FindAsync(id);
            context.CitiesCoordinates.Remove(city!);
            await context.SaveChangesAsync();
        }
    }
}
