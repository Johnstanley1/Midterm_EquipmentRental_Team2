using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;
using Midterm_EquipmentRental_Team2.Repositories;

namespace Midterm_EquipmentRental_Team2.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }


        public IEnumerable<Rental> GetAllRentals()
        {
            return _rentalRepository.GetAll();
        }

        public Rental GetRentalsById(int rentalId)
        {
            return _rentalRepository.GetById(rentalId);
        }

        public IEnumerable<Enums> GetStatus()
        {
            return _rentalRepository.GetStatus();
        }

        public IEnumerable<Rental> GetRentedEquipments(int equipmentId)
        {
            return _rentalRepository.GetRented(equipmentId);
        }

        public IEnumerable<Rental> GetActiveRentals(int equipmentId)
        {
            return _rentalRepository.GetActive(equipmentId);
        }

        public IEnumerable<Rental> GetCompletedRentals(int equipmentId)
        { 
            return _rentalRepository.GetCompleted(equipmentId);
        }

        public IEnumerable<Rental> GetOverdueRentals()
        {
            return _rentalRepository.GetOverdue();
        }

        public void AddRental(Rental rental)
        {
            _rentalRepository.Add(rental); 
        }

        public void ReturnRental(Rental rental)
        {
            _rentalRepository.Return(rental);
        }

        public void UpdateRental(Rental rental)
        {
            _rentalRepository.Update(rental);
        }

        public void DeleteRental(Rental rental)
        {
            _rentalRepository.Delete(rental);
        }

        private static string ComputeStatus(Rental r)
        {
            if (r.ReturnedAt != null) return "Returned";
            if (r.DueDate.HasValue && r.DueDate.Value < DateTime.UtcNow) return "Overdue";
            return "Active";
        }
    }
}
