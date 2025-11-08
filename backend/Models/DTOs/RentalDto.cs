using System;
using static Midterm_EquipmentRental_Team2.Models.Equipment;
using static Midterm_EquipmentRental_Team2.Models.Rental;

namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string? ReturnNotes { get; set; }
        public String? Status { get; set; }
        public String? EquipmentCondition { get; set; }
        public String? EquipmentStatus { get; set; }
        public int CustomerId { get; set; }
        public int EquipmentId { get; set; }
        public String? CustomerName { get; set; }
        public String? CustomerEMail { get; set; }
        public String? EquipmentName { get; set; }
        public Equipment Equipment { get; set; } = new();
    }
}
