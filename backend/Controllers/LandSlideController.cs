using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LandSlideController: ControllerBase
    {
        private readonly WeatherService _weatherService;

        private readonly PredictionService _predictionService;
        private readonly AppDbContext _dbContext;



        public LandSlideController(WeatherService weatherService, PredictionService predictionService, AppDbContext dbContext)
        {
            _weatherService = weatherService;
            _predictionService = predictionService;
            _dbContext = dbContext;
        }


        [HttpGet("check")]
        public async Task<IActionResult> CheckLandslideRisk(string cityName)
        {
            var (totalRainfall, rainfallData) = await _weatherService.GetThreeDayRainfallAmount(cityName);

            var riskLevel =_predictionService.DetermineRiskLevel(totalRainfall, rainfallData);

            var hotSpot = await _dbContext.HotSpots.FindAsync(cityName);
            if (hotSpot == null)
            {
                hotSpot = new HotSpot
                {
                    CityName = cityName,
                    lastRainFallAmount = totalRainfall,
                    RiskLevel = riskLevel,
                    LastUpdated = DateTime.UtcNow,
                    SearchCount = 1
                };
                _dbContext.HotSpots.Add(hotSpot);
            }
            else
            {
                hotSpot.lastRainFallAmount = totalRainfall;
                hotSpot.RiskLevel = riskLevel;
                hotSpot.LastUpdated = DateTime.UtcNow;
                hotSpot.SearchCount += 1;
                _dbContext.HotSpots.Update(hotSpot);
            }

            await _dbContext.SaveChangesAsync();

            return Ok(new { CityName = cityName, RainfallAmount = totalRainfall, RiskLevel = riskLevel });
        }
    }
}