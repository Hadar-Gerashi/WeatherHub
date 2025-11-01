using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using WeatherHub.Shared.DTOs;
using WeatherHubClient.Models;
using WeatherHubClient.Services.Api;
using WeatherHubClient.Services.Facades;
using WeatherHubClient.Services.Repositories;
using WeatherHubClient.Utils;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly WeatherService weatherService;
    private readonly CityService cityService;

    public ObservableCollection<City> FavoriteCities { get; } = new();
    public ObservableCollection<City> RecentCities { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private WeatherDataDto? currentWeatherData;
    private bool _isLoading;

    public bool IsLoading
    {
        get => _isLoading;
        set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
    }

    public WeatherDataDto? CurrentWeatherData
    {
        get => currentWeatherData;
        set
        {
            if (currentWeatherData != value)
            {
                currentWeatherData = value;
                OnPropertyChanged(nameof(CurrentWeatherData));
                OnPropertyChanged(nameof(CurrentHourlyForecast));
                OnPropertyChanged(nameof(PolylinePoints));
                OnPropertyChanged(nameof(PolygonPoints));
                OnPropertyChanged(nameof(RetrievedAtLocal));
           
            }
        }
    }

    public HourlyForecastDto? CurrentHourlyForecast =>
        CurrentWeatherData?.HourlyForecasts?
            .OrderBy(f => Math.Abs((f.ForecastTime - DateTime.Now).TotalMinutes))
            .FirstOrDefault();


    public string RetrievedAtLocal =>
        CurrentWeatherData?.RetrievedAt?.ToLocalTime().ToString("dddd HH:mm", CultureInfo.InvariantCulture) ?? "";

    private double canvasHeight = 120;
    private double canvasWidth = 650;

    public string PolylinePoints => GraphUtils.CalculatePolyline(CurrentWeatherData?.HourlyForecasts?.Select(f => f.Temperature) ?? Enumerable.Empty<double>(), canvasWidth, canvasHeight);
    public string PolygonPoints => GraphUtils.CalculatePolygon(CurrentWeatherData?.HourlyForecasts?.Select(f => f.Temperature) ?? Enumerable.Empty<double>(), canvasWidth, canvasHeight);

    public MainViewModel(WeatherService weatherService, CityService cityService)
    {
        this.weatherService = weatherService;
        this.cityService = cityService;

        LoadCities();
    }

    private void LoadCities()
    {
        FavoriteCities.Clear();
        foreach (var c in cityService.GetFavorites()) FavoriteCities.Add(c);

        RecentCities.Clear();
        foreach (var c in cityService.GetRecents()) RecentCities.Add(c);
    }

    public void AddFavorite(string cityName)
    {
        var added = cityService.AddFavorite(cityName);
        if (added != null) FavoriteCities.Insert(0, added);
    }

    public void AddRecent(string cityName)
    {
        var updated = cityService.AddOrUpdateRecent(cityName);
        RecentCities.Remove(updated);
        RecentCities.Insert(0, updated);
        while (RecentCities.Count > 25)
            RecentCities.RemoveAt(RecentCities.Count - 1);
    }


    public void RemoveFavorite(City city)
    {
        cityService.RemoveFavorite(city);
        FavoriteCities.Remove(city);

    }

    public async Task<WeatherDataDto?> LoadWeatherAsync(string cityName)
    {
        try
        {
            IsLoading = true;
            var data = await weatherService.GetWeatherAsync(cityName);
            CurrentWeatherData = data;
            return data;
        }
        finally { IsLoading = false; }
    }

    public RecentCity? GetLastCity() => cityService.GetLastCity();

  


}
