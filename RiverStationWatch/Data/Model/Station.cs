using System.ComponentModel.DataAnnotations;

namespace RiverStationWatch.Data.Model
{
    public class Station
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string StationName { get; set; } // River name / station name
        [Required]
        public int TimeOutInMinutes { get; set; } // (240)
        [Required]
        [Range(0, 100)]
        public double DroughtLevel { get; set; } // 0-30
        [Required]
        [Range(0, 100)]
        public double FloodLevel { get; set; } // 71 - 100
        [EmailAddress]
        public string? ReportEmail { get; set; }
    }
}
