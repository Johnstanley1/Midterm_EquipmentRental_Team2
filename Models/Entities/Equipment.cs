using System.ComponentModel.DataAnnotations;

namespace Midterm_EquipmentRental_Team2.Models
{
    public class Equipment
    {
        // equipment category enums
        public enum EquipmentCategory
        {
            HeavyMachinery,
            PowerTools,
            Vehicles,
            Safety,
            Surveying
        }


        // equipment conditon enums
        public enum EquipmentCondition
        {
            New,
            Excellent,
            Good,
            Fair,
            Poor
        }


        // equipment status enums
        public enum EquipmentStatus
        {
            Available,
            Rented,
            Maintenance
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsAvailable { get; set; } = true;

        [MaxLength(20)]
        public EquipmentStatus Status { get; set; }

        [Required]
        public EquipmentCategory Category { get; set; }

        [Required]
        public EquipmentCondition Condition { get; set; }

        //// Navigation property for rentals
        //public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    }
}
