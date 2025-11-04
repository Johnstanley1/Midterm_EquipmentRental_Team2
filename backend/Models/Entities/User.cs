using System.ComponentModel.DataAnnotations;

namespace Midterm_EquipmentRental_Team2.Models
{
    // user model
    public class User
    {
        public int Id { get; set; }

        // [Required(ErrorMessage = "Username is required to login")]
        // [MaxLength(50)]
        // public string Username { get; set; }

        // [Required(ErrorMessage = "Please enter password to login")]
        // [MaxLength(50)]
        // public string Password { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; } = true;

        public string Email { get; set; }
         public string? ExternalProvider { get; set; } = "Google";
        public string? ExternalId { get; set; }

    }
}
