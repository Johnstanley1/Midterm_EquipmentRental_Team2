namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<RentalSummaryDto> Rentals { get; set; } = new();
    }
}
