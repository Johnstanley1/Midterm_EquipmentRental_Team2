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


        public IEnumerable<RentalDTO> GetAllRentals()
        {
            return _rentalRepository.GetAll();
        }

        public RentalDTO GetRentalsById(int rentalId)
        {
            return _rentalRepository.GetById(rentalId);
        }

        public Rental GetRentalEntityById(int rentalId)
        {
            return _rentalRepository.GetByEntityId(rentalId);
        }

        public IEnumerable<Enums> GetStatus()
        {
            return _rentalRepository.GetStatus();
        }

        public IEnumerable<RentalDTO> GetRentedEquipments(int id)
        {
            return _rentalRepository.GetRented(id);
        }

        public IEnumerable<RentalDTO> GetActiveRentals()
        {
            return _rentalRepository.GetActive();
        }

        public IEnumerable<RentalDTO> GetCompletedRentals()
        { 
            return _rentalRepository.GetCompleted();
        }

        public IEnumerable<RentalDTO> GetOverdueRentals()
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
