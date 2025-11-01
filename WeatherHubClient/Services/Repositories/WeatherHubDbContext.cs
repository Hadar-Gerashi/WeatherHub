using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHubClient.Models;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace WeatherHubClient.Services.Repositories
{
    public class WeatherHubDbContext : DbContext
    {
        public DbSet<FavoriteCity> FavoriteCities { get; set; }
        public DbSet<RecentCity> RecentCities { get; set; }

        public WeatherHubDbContext(DbContextOptions<WeatherHubDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteCity>()
                 .HasIndex(c => c.Name)
                 .IsUnique();


            modelBuilder.Entity<RecentCity>()
                        .HasIndex(c => c.Name)
                        .IsUnique();
        }
    }
}
