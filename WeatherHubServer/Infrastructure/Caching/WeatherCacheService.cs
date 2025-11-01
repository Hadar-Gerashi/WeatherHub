using Core.Interfaces.External;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHub.Shared.DTOs;

namespace Infrastructure.Caching
{


    public class WeatherCacheService : IWeatherCacheService
    {
        private readonly IMemoryCache cache;
        private readonly TimeSpan cacheDuration = TimeSpan.FromHours(12); 

        public WeatherCacheService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public Task<WeatherDataDto?> GetAsync(string cityName)
        {
            cache.TryGetValue(cityName, out WeatherDataDto? data);
            return Task.FromResult(data);
        }

        public Task SetAsync(string cityName, WeatherDataDto data)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuration
            };
            cache.Set(cityName, data, options);
            return Task.CompletedTask;
        }
    }
}
