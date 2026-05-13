using backend.Data;


namespace backend.Services
{
    public class PredictionService
    {
        private readonly AppDbContext _dbContext;

        public PredictionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // public string DetermineRiskLevel(double rainfallAmount)
        // {
        //     if (rainfallAmount < 20)
        //         return "Low";
        //     else if (rainfallAmount < 50)
        //         return "Medium";
        //     else
        //         return "High";
        // }
        
        public string DetermineRiskLevel(double rainfallAmount, Dictionary<string, double> threeDayWeatherHistory)
        {
            // Implementation for determining risk level based on three-day weather history
            var sortedRainFall = threeDayWeatherHistory.OrderBy(x => x.Key).Select(x => x.Value).ToList();

            if (sortedRainFall.Count < 3)
            {
                // Not enough data to determine risk level, default to Low
                return "Low";
            }

            double day1Rainfall = sortedRainFall[0];
            double day2Rainfall = sortedRainFall[1];
            double twoDayAverage = (day1Rainfall + day2Rainfall) / 2;
            double thirdDayRainfall = sortedRainFall[2];

            if (twoDayAverage > 50 && thirdDayRainfall > 50)
                return "Very High";
            else if  (twoDayAverage > 50 && thirdDayRainfall > 10)
                return "High";
            else if (twoDayAverage > 20 && thirdDayRainfall > 40)
                return "Medium";
            else
                return "Low";
        }
    }
}