using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Midterm_EquipmentRental_Team2.Models;

namespace Midterm_EquipmentRental_Team2.Models
{
    public class Rental
    {
        public enum RentalStatus 
        {
            Active, 
            Returned, 
            Overdue, 
            Cancelled
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Issue date is required")]
        public DateTime IssuedAt { get; set; }


        [Required(ErrorMessage = "Due date is required")]
        public DateTime? DueDate { get; set; }

        public DateTime? ReturnedAt { get; set; }

        [MaxLength(255)]
        public string? ReturnNotes { get; set; }

        public int CustomerId { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }
        public int EquipmentId { get; set; }

        [JsonIgnore]
        public Equipment? Equipment { get; set; }

        public Equipment.EquipmentCondition? EquipmentCondition { get; set; }
        public Equipment.EquipmentStatus? EquipmentStatus { get; set; }
        public RentalStatus Status { get; set; }
    }
}
