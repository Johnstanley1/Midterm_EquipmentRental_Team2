using static Midterm_EquipmentRental_Team2.Models.Customer;

namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public List<RentalDTO> Rentals { get; set; } = new();
    }
}
