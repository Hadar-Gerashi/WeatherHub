using Core.Interfaces.External;
using Core.Interfaces.Infrastructure;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Data;
using Data.Repositories;
using Infrastructure.Caching;
using Infrastructure.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Implementations;
using Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Composition.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherServices(this IServiceCollection services, string connectionString)
        {

            services.AddHttpClient<ICityCoordinateApiClient, CityCoordinateApiClient>();

    
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

         
            services.AddScoped<ICityCoordinateRepository, CityCoordinateRepository>();
            services.AddScoped<ICityCoordinateService, CityCoordinateService>();

            services.AddMemoryCache();
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IWeatherCacheService, WeatherCacheService>();
            services.AddHttpClient<IWeatherApiClient, WeatherApiClient>();
 





            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });


          


            return services;
        }
    }
}
