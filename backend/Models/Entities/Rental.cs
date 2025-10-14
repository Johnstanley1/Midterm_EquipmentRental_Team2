using System.ComponentModel.DataAnnotations;
using Midterm_EquipmentRental_Team2.Models;

namespace Midterm_EquipmentRental_Team2.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        [Required]
        public DateTime IssuedAt { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ReturnedAt { get; set; }

        [MaxLength(255)]
        public string? ReturnNotes { get; set; }

        public Equipment.EquipmentCondition? ReturnCondition { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active"; // Active, Completed, Overdue, Cancelled
    }
}
