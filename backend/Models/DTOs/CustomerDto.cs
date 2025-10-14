namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public bool IsActive { get; set; } = true;
    }
}
