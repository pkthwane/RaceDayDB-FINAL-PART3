using System.ComponentModel.DataAnnotations;

namespace RaceDayDB.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public int RaceID { get; set; }
    }
}