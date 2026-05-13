using System.Net.Http.Json;
using backend.Data;
using backend.Models;


namespace backend.Services
{

    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly String _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WEATHER_API_KEY"]!;
        }

        /// <summary>
        /// Fetches the rainfall amount for a given city using a weather API. This is a placeholder implementation and should be replaced with actual API calls and response parsing.
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
    public async Task<double> GetRainfallAmount(string cityName)
        {

            var response = await _httpClient.GetAsync($"https://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}");
            if (response.IsSuccessStatusCode)
            {
                var weatherData = await response.Content.ReadFromJsonAsync<WeatherInfo>();
                return weatherData?.Current.PrecipMm ?? 0.0;
            }
            else
            {
                // Handle error response
                Console.WriteLine($"Failed to fetch weather data for {cityName}. Status Code: {response.StatusCode}");
                throw new Exception($"Failed to fetch weather data for {cityName}. Status Code: {response.StatusCode}");
                 // Default to 0 if API call fails
            }
        }

        // Change this line:
        public async Task<(double totalRainfall, Dictionary<string, double> rainfallData)> GetThreeDayRainfallAmount(string cityName)
        {
            // Get last 3 days rainfall amount from weather API
            Dictionary<string, double> rainfallData = new Dictionary<string, double>();
            double totalRainfall = 0.0;
            for (int i = 1; i <= 3; i++)
            {
                var response = await _httpClient.GetAsync($"https://api.weatherapi.com/v1/history.json?key={_apiKey}&q={cityName}&dt={DateTime.UtcNow.AddDays(-i).ToString("yyyy-MM-dd")}");
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"WEATHER_API_KEY present: {!string.IsNullOrEmpty(_apiKey)}");
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Body: {json}");


                if (response.IsSuccessStatusCode)
                {
                    var weatherData = await response.Content.ReadFromJsonAsync<WeatherInfo>();
                    double dailyRainfall = weatherData?.Forecast?.Days?.FirstOrDefault()?.Day?.TotalPrecipMm ?? 0.0;
                    rainfallData.Add(DateTime.UtcNow.AddDays(-i).ToString("yyyy-MM-dd"), dailyRainfall);
                    totalRainfall += dailyRainfall;
                }
                else
                {
                    // Handle error response
                    Console.WriteLine($"Failed to fetch weather data for {cityName} on {DateTime.UtcNow.AddDays(-i).ToString("yyyy-MM-dd")}. Status Code: {response.StatusCode}");
                    rainfallData.Add(DateTime.UtcNow.AddDays(-i).ToString("yyyy-MM-dd"), 0.0); // Default to 0 if API call fails
                }
            }

            return (totalRainfall, rainfallData);
        }
    }
}