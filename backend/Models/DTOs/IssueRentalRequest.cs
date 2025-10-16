using System;

namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class IssueRentalRequest
    {
        public int EquipmentId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? IssuedAt { get; set; }
    }
}