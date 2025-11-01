using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHubClient.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherHubClient.Services.Repositories
{

    public class FavoriteCitiesService
    {

        private readonly WeatherHubDbContext context;
        private const int MaxFavorites = 8;


        public FavoriteCitiesService(WeatherHubDbContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
        }


        public List<FavoriteCity> GetFavoriteCities()
        {
            return context.FavoriteCities
                          .Take(MaxFavorites)
                          .ToList();
        }


        public FavoriteCity? AddFavoriteCity(FavoriteCity city)
        {
            if (context.FavoriteCities.Count() >= MaxFavorites ||
                context.FavoriteCities.Any(c => c.Name == city.Name))
            {
                return null;
            }

            context.FavoriteCities.Add(city);
            context.SaveChanges();
            return city; 
        }


        public void RemoveFavoriteCity(City city)
        {
            var existing = context.FavoriteCities.FirstOrDefault(c => c.Id == city.Id);
            if (existing != null)
            {
                context.FavoriteCities.Remove(existing);
                context.SaveChanges();
            }
        }
    }
}

