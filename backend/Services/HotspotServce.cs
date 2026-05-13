using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;


namespace backend.Services
{

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