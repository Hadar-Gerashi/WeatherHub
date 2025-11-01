using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHubClient.Models;
using WeatherHubClient.Services.Repositories;

namespace WeatherHubClient.Services.Facades
{
    public class CityService
    {
        private readonly FavoriteCitiesService favoriteService;
        private readonly RecentCitiesService recentService;

        public CityService(FavoriteCitiesService favService, RecentCitiesService recService)
        {
            favoriteService = favService;
            recentService = recService;
        }

        public IEnumerable<City> GetFavorites() => favoriteService.GetFavoriteCities();
        public IEnumerable<City> GetRecents() => recentService.GetRecentCities();

        public City? AddFavorite(string cityName)
        {
            var city = new FavoriteCity { Name = cityName };
            return favoriteService.AddFavoriteCity(city);
        }

        public City AddOrUpdateRecent(string cityName)
        {
            return recentService.AddOrUpdateCity(cityName);
        }

        public void RemoveFavorite(City city)
        {
            favoriteService.RemoveFavoriteCity(city);
        }

        public RecentCity? GetLastCity()
        {
            return recentService.GetLastCity();
        }


    }

}
