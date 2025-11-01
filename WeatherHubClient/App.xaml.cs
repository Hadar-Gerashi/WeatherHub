using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using System.Configuration;
using System.Data;
using System.Windows;
using WeatherClient.Views;
using WeatherHubClient.External.Implementations;
using WeatherHubClient.External.Interfaces;
using WeatherHubClient.Services.Api;
using WeatherHubClient.Services.Facades;
using WeatherHubClient.Services.Repositories;

namespace WeatherClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

            private IHost? host;

        protected override void OnStartup(StartupEventArgs e)
        {

            var dbPath = ConfigurationManager.AppSettings["CitiesDBPath"];
            var apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"]!;

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
               
                    services.AddHttpClient<IWeatherApiClient, WeatherApiClient>(client =>
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                        client.Timeout = TimeSpan.FromSeconds(30);
                    });

                 
                    services.AddDbContext<WeatherHubDbContext>(options =>
                        options.UseSqlite($"Data Source={dbPath}"));

    
                    services.AddSingleton<FavoriteCitiesService>();
                    services.AddSingleton<RecentCitiesService>();
                    services.AddSingleton<WeatherService>();
                    services.AddSingleton<CityService>();

                    services.AddTransient<MainViewModel>();
                })
                .Build();

            host.Start();

            var viewModel = host.Services.GetRequiredService<MainViewModel>();
            var mainWindow = new MainWindow(viewModel);
            mainWindow.Show();

        }


        protected override async void OnExit(ExitEventArgs e)
        {
            if (host != null)
            {
                await host.StopAsync();
                host.Dispose();
            }
            base.OnExit(e);
        }

    }


}
