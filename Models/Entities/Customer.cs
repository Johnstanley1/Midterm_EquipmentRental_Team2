//using System.ComponentModel.DataAnnotations;

//namespace Midterm_EquipmentRental_Team2.Models
//{
//    public class Customer
//    {
//        [Key]
//        public int Id { get; set; }

//        [Required]
//        [MaxLength(100)]
//        public string Name { get; set; } = string.Empty;

//        [Required]
//        [MaxLength(50)]
//        public string Username { get; set; } = string.Empty;

//        [Required]
//        [MaxLength(100)]
//        public string Password { get; set; } = string.Empty;

//        [Required]
//        [MaxLength(20)]
//        public string Role { get; set; } = "User"; // "Admin" or "User"

//        public bool IsActive { get; set; } = true;


//        // Navigation property for rentals
//        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
//    }
//}
