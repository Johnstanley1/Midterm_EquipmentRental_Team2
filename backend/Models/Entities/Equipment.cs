using Midterm_EquipmentRental_Team2.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Midterm_EquipmentRental_Team2.Models
{
    public class Equipment
    {
        public enum EquipmentCategory
        {
            HeavyMachinery,
            PowerTools,
            Vehicles,
            Safety,
            Surveying
        }

        public enum EquipmentCondition 
        {   
            New,
            Excellent,
            Good,
            Fair,
            Poor
        }

        public enum EquipmentStatus
        {
            Available,
            Rented,
            Maintenance
        }


        public int Id { get; set; }

        [Required(ErrorMessage = "Equipment name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Equipment description is required")]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;


        [Required(ErrorMessage = "Equipment status is required")]
        public EquipmentStatus Status { get; set; } 


        [Required(ErrorMessage = "Equipment category is required")]
        public EquipmentCategory Category { get; set; }


        [Required(ErrorMessage = "Equipment condition is required")]
        public EquipmentCondition Condition { get; set; }

        //// Navigation property for rentals
        //public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    }
}
