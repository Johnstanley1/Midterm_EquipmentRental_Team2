using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Services
{
    public interface IRentalService
    {
        IEnumerable<RentalDTO> GetAllRentals();
        IEnumerable<Enums> GetStatus();
        IEnumerable<RentalDTO> GetRentedEquipments(int id);
        IEnumerable<RentalDTO> GetActiveRentals();
        IEnumerable<RentalDTO> GetCompletedRentals();
        IEnumerable<RentalDTO> GetOverdueRentals();
        RentalDTO? GetRentalsById(int rentalId);
        Rental GetRentalEntityById(int id);
        void AddRental(Rental rental);
        void UpdateRental(Rental rental);
        void ReturnRental(Rental rental);
        void DeleteRental(Rental rental);   
    }
}
