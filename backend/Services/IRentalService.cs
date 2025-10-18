using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Services
{
    public interface IRentalService
    {
        IEnumerable<Rental> GetAllRentals();
        IEnumerable<Enums> GetStatus();
        IEnumerable<Rental> GetRentedEquipments(int equipmentId);
        IEnumerable<Rental> GetActiveRentals(int equipmentId);
        IEnumerable<Rental> GetCompletedRentals(int equipmentId);
        IEnumerable<Rental> GetOverdueRentals();
        Rental? GetRentalsById(int rentalId);
        void AddRental(Rental rental);
        void UpdateRental(Rental rental);
        void ReturnRental(Rental rental);
        void DeleteRental(Rental rental);   
    }
}
