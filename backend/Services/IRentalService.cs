using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Services
{
    public interface IRentalService
    {
        IEnumerable<Rental> GetAllRentals(int? userId = null, bool isAdmin = false);
        Rental? GetRental(int id, int? userId = null, bool isAdmin = false);
        IEnumerable<RentalDto> GetAllRentalsDto(int? userId = null, bool isAdmin = false);
        RentalDto? GetRentalDto(int id, int? userId = null, bool isAdmin = false);
        IEnumerable<Rental> GetRentalsByEquipment(int equipmentId);
        IEnumerable<Rental> GetActiveRentals(int? userId = null, bool isAdmin = false);
        IEnumerable<Rental> GetCompletedRentals(int? userId = null, bool isAdmin = false);
        IEnumerable<Rental> GetOverdueRentals();
        void IssueRental(Rental rental, int userId);
        void ReturnRental(int rentalId, int userId, string? notes, string? condition, bool force = false);
        void ExtendRental(int rentalId, DateTime newDueDate, string reason, int userId);
        void CancelRental(int rentalId, int userId, bool force = false);
    }
}
