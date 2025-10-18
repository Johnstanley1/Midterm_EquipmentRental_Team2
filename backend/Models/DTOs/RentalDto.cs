using System;

namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Status { get; set; }
        public string EquipmentCondition { get; set; }
        public string EquipmentStatus { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string EquipmentName { get; set; }
        public List<Equipment> Equipments { get; set; } = new();
    }
}
