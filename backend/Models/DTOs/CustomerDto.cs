namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<RentalDTO> Rentals { get; set; } = new();
    }
}
