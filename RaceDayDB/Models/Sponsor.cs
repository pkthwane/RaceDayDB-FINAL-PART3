using System.ComponentModel.DataAnnotations;

namespace RaceDayDB.Models
{
    public class Sponsor
    {
        [Key]
        public int SponsorID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}