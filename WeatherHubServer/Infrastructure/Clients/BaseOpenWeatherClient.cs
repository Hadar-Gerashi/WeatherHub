using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Clients
{
    public abstract class BaseOpenWeatherClient
    {
        protected readonly HttpClient httpClient;
        protected readonly string apiKey;
        protected readonly string baseUrl;

        protected BaseOpenWeatherClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            apiKey = configuration["OpenWeather:ApiKey"]!;
            baseUrl = configuration["OpenWeather:GeoUrl"]!;
        }

        protected async Task<JsonDocument> GetJsonAsync(string relativeUrl)
        {
            try
            {
                var response = await httpClient.GetAsync($"{baseUrl}{relativeUrl}&appid={apiKey}&units=metric");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonDocument.Parse(json);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Network error or server unreachable.", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception("Request timed out.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while fetching data.", ex);
            }
        }
    }
}
