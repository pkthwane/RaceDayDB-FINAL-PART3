using System.ComponentModel.DataAnnotations;

namespace RaceDayDB.Models
{
    public class RaceSponsor
    {
        [Key]
        public int RaceSponsorID { get; set; }

        public int RaceID { get; set; }

        public int SponsorID { get; set; }

        public decimal SponsorshipAmount { get; set; }
    }
}