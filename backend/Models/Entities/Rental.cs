using System.ComponentModel.DataAnnotations;
using Midterm_EquipmentRental_Team2.Models;

namespace Midterm_EquipmentRental_Team2.Models
{
    public class Rental
    {
        public enum RentalStatus 
        {
            Active, 
            Completed, 
            Overdue, 
            Cancelled
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Issue date is required")]
        public DateTime IssuedAt { get; set; }


        [Required(ErrorMessage = "Due date is required")]
        public DateTime? DueDate { get; set; }


        [Required(ErrorMessage = "Return date is required")]
        public DateTime? ReturnedAt { get; set; }


        [Required(ErrorMessage = "Return note is required")]
        [MaxLength(255)]
        public string? ReturnNotes { get; set; }

        public Equipment.EquipmentCondition? ReturnCondition { get; set; }
        public IEnumerable<Equipment> Equipments { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public RentalStatus Status { get; set; }
    }
}
