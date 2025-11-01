using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHubClient.Models;

namespace WeatherHubClient.Services.Repositories
{
    public class RecentCitiesService
    {

        private readonly WeatherHubDbContext context;
        private const int MaxRecent = 25;

 
        public RecentCitiesService(WeatherHubDbContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
        }


        public List<RecentCity> GetRecentCities()
        {
            return context.RecentCities
                           .OrderByDescending(c => c.AddedAt)
                           .Take(MaxRecent)
                           .ToList();
        }

        public RecentCity? GetLastCity()
        {
            return context.RecentCities
                          .OrderByDescending(c => c.AddedAt)
                          .FirstOrDefault();
        }


        public RecentCity AddRecentCity(string cityName)
        {
            var newCity = new RecentCity { Name = cityName, AddedAt = DateTime.Now };
            context.RecentCities.Add(newCity);
            context.SaveChanges();

            RemoveOldRecentCities(); 

            return newCity;
        }



        private void RemoveOldRecentCities()
        {
            var count = context.RecentCities.Count();
            if (count > MaxRecent)
            {
                var toRemove = context.RecentCities
                                       .OrderBy(c => c.AddedAt)
                                       .Take(count - MaxRecent)
                                       .ToList();
                context.RecentCities.RemoveRange(toRemove);
                context.SaveChanges();
            }
        }

        public RecentCity UpdateRecentCity(string cityName)
        {
            var existing = context.RecentCities.FirstOrDefault(c => c.Name == cityName)!;
            existing.AddedAt = DateTime.Now;
            context.RecentCities.Update(existing);
            context.SaveChanges();
            return existing;
        }

        public RecentCity AddOrUpdateCity(string cityName)
        {
            var existing = context.RecentCities.FirstOrDefault(c => c.Name == cityName);
            if (existing != null)
                return UpdateRecentCity(cityName);
            return AddRecentCity(cityName);
        }

    }

}
