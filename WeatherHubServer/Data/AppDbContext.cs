using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<CityCoordinate> CitiesCoordinates { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CityCoordinate>()
                .ToTable("CityCoordinates");

            modelBuilder.Entity<CityCoordinate>()
                .OwnsOne(c => c.Coordinates, coord =>
                {
                    coord.Property(c => c.Latitude).HasColumnName("Latitude");
                    coord.Property(c => c.Longitude).HasColumnName("Longitude");
                });
        }
    }
}
