using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;


namespace backend.Services
{
    /// <summary>
    /// This service is responsible for handling the logic related to hotspots, including fetching weather data,  and saving results to the database.
    /// </summary>
    public class HotspotService
    {
        private readonly AppDbContext _dbContext;
        private readonly PredictionService _predictionService;
        private readonly WeatherService _weatherService;

        public HotspotService(AppDbContext dbContext, PredictionService predictionService, WeatherService weatherService)
        {
            _dbContext = dbContext;
            _predictionService = predictionService;
            _weatherService = weatherService;

        }

        /// <summary>
        /// Gets the hotspot (weather variables) information for a given city from the external weather API
        /// and saves the hotspot to the DB.
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns>The hotspot record for the specified city</returns>
        public async Task<HotSpot> GetHotspotForCity(string cityName)
        {
            var cachedRecord = await _dbContext.HotSpots.FirstOrDefaultAsync(h => h.CityName.ToLower() == cityName.ToLower() && h.RecordDate.Date == DateTime.UtcNow.Date);

            if (cachedRecord != null)
            {
                return cachedRecord;
            }
            else
            {
                var (totalRainfall, rainfallData) = await _weatherService.GetThreeDayRainfallAmount(cityName);
                var riskLevel = _predictionService.DetermineRiskLevel(totalRainfall, rainfallData);

                var newRecord = new HotSpot
                {
                    Id = Guid.NewGuid(),
                    CityName = cityName,
                    lastRainFallAmount = totalRainfall,
                    RiskLevel = riskLevel,
                    RecordDate = DateTime.UtcNow,
                };

                    
                _dbContext.HotSpots.Add(newRecord);
                await _dbContext.SaveChangesAsync();

                return newRecord;
            }
        }

        
    }
}