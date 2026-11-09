using System.ComponentModel.DataAnnotations;
namespace RaceDayDB.Models
{
    public class Organizer
    {
        [Key]
        public int OrgID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ContactInfo { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Organizer";

    }
}
