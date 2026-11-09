using System.ComponentModel.DataAnnotations;

namespace RaceDayDB.Models
{
    public class Registration
    {
        [Key]
        public int RegID { get; set; }

        public int RunnerID { get; set; }

        public int RaceID { get; set; }

        public int CategoryID { get; set; }

        public string PaymentStatus { get; set; } = string.Empty;
    }
}