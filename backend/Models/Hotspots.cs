using System.ComponentModel.DataAnnotations;



namespace backend.Models
{   
    /// <summary>
    /// This class determines the structure of the hotspot DB table.
    /// </summary>
    public class HotSpot
    {
        [Key]
        public Guid Id { get; set; }
        public string CityName { get; set; } = "";
        public double lastRainFallAmount { get; set; }

        public string RiskLevel { get; set; } = "";

        public DateTime RecordDate { get; set; }

        public enum Source
        {
            API,
            Cache
        }
    }
}