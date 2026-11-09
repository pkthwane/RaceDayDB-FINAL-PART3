using System.ComponentModel.DataAnnotations;

namespace RaceDayDB.Models
{
    public class Result
    {
        [Key]
        public int ResultID { get; set; }

        public int RunnerID { get; set; }

        public int RaceID { get; set; }

        public TimeSpan FinishTime { get; set; }

        public int Position { get; set; }
    }
}