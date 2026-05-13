using System.ComponentModel.DataAnnotations;



namespace backend.Models
{
    public class HotSpot
    {
    [Key]
    public string CityName { get; set; } = "";
    public double lastRainFallAmount { get; set; }

    public string RiskLevel { get; set; } = "";

    public DateTime LastUpdated { get; set; }

    public int SearchCount { get; set; }
    }
}