using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace backend.Models

{
    public class WeatherInfo
    {
        [JsonPropertyName("current")]
        public CurrentWeather Current { get; set; } = new CurrentWeather();

        [JsonPropertyName("forecast")]
        public ForecastContainer? Forecast { get; set; }
    }

    public class CurrentWeather
    {
        [JsonPropertyName("precip_mm")]
        public double PrecipMm { get; set; }
    }

    public class ForecastContainer
    {
        [JsonPropertyName("forecastday")]
        public List<ForecastDayItem> Days { get; set; } = new();
    }

    public class ForecastDayItem
    {
        [JsonPropertyName("day")]
        public DayData Day { get; set; } = new DayData();
    }

    public class DayData
    {
        [JsonPropertyName("totalprecip_mm")]
        public double TotalPrecipMm { get; set; }
    }
}