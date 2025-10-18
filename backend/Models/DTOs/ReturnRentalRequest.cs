namespace Midterm_EquipmentRental_Team2.Models.DTOs
{
    public class ReturnRentalRequest
    {
        public int Id { get; set; }
        public string? ReturnNotes { get; set; }
        public string? ReturnCondition { get; set; }
    }
}
