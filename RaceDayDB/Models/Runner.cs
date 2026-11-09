using System.ComponentModel.DataAnnotations;

namespace RaceDayDB.Models
{
    public class Runner
    {
        [Key]
        public int RunnerID { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string ContactInfo { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Runner";

        public string? ProfileImageUrl { get; set; }
    }
}