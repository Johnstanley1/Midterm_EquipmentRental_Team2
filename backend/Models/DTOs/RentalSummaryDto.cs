namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class RentalSummaryDto
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string EquipmentStatus { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
