namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class RentalDto
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Status { get; set; } = "Active";
    }
}
