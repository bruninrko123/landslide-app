using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LandSlideController : ControllerBase
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

            try
            {
                var hotspotService = new HotspotService(_dbContext, _predictionService, _weatherService);
                var hotspot = await hotspotService.GetHotspotForCity(cityName);
                return Ok(hotspot);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking landslide risk for {cityName}: {ex.Message}");
                return StatusCode(500, $"Error checking landslide risk for {cityName}: {ex.Message}");
            }





        }





    }
}
