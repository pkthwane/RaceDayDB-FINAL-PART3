using System.ComponentModel.DataAnnotations;

namespace RaceDayDB.Models
{
    public class Race
    {
        [Key]
        public int RaceID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string EventType { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Location { get; set; } = string.Empty;

        public decimal Distance { get; set; }

        public int OrgID { get; set; }

        public string? BannerImageUrl { get; set; }
    }
}