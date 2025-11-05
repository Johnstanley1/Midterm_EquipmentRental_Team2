using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Midterm_EquipmentRental_Team2.Models
{
    public class Customer
    {
        public enum UserRole 
        { 
            Admin,
            User
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Customer email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password  is required")]
        [MaxLength(100)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Role type is required")]
        public UserRole Role { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>(); // Navigation property for rentals

        //[Required(ErrorMessage = "Username is required")]
        //[MaxLength(50)]
        //public string Username { get; set; }
    }
}
