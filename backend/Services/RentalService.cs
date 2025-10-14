using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Repositories;

namespace Midterm_EquipmentRental_Team2.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IEquipementRepository _equipmentRepo;

        public RentalService(IRentalRepository rentalRepo, ICustomerRepository customerRepo, IEquipementRepository equipmentRepo)
        {
            _rentalRepo = rentalRepo;
            _customerRepo = customerRepo;
            _equipmentRepo = equipmentRepo;
        }

        public IEnumerable<Rental> GetAllRentals(int? userId = null, bool isAdmin = false)
        {
            return isAdmin ? _rentalRepo.GetAll() : _rentalRepo.GetByCustomerId(userId ?? 0);
        }

        public Rental? GetRental(int id, int? userId = null, bool isAdmin = false)
        {
            var rental = _rentalRepo.GetById(id);
            if (rental == null) return null;
            if (!isAdmin && rental.CustomerId != userId) return null;
            return rental;
        }

        public IEnumerable<Rental> GetRentalsByEquipment(int equipmentId) => _rentalRepo.GetByEquipmentId(equipmentId);

        public IEnumerable<Rental> GetActiveRentals(int? userId = null, bool isAdmin = false)
        {
            return isAdmin ? _rentalRepo.GetAll().Where(r => r.ReturnedAt == null) : _rentalRepo.GetActiveByCustomerId(userId ?? 0);
        }

        public IEnumerable<Rental> GetCompletedRentals(int? userId = null, bool isAdmin = false)
        {
            return isAdmin ? _rentalRepo.GetAll().Where(r => r.ReturnedAt != null) : _rentalRepo.GetCompletedByCustomerId(userId ?? 0);
        }

        public IEnumerable<Rental> GetOverdueRentals() => _rentalRepo.GetOverdue();

        public void IssueRental(Rental rental, int userId)
        {
            // Only one active rental per user
            if (_rentalRepo.GetActiveByCustomerId(userId).Any())
                throw new InvalidOperationException("User already has an active rental.");
            // Equipment must be available
            var equipment = _equipmentRepo.GetById(rental.EquipmentId);
            if (equipment == null || !equipment.IsAvailable)
                throw new InvalidOperationException("Equipment not available.");
            equipment.IsAvailable = false;
            equipment.Status = Equipment.EquipmentStatus.Rented;
            _equipmentRepo.Update(equipment);
            rental.CustomerId = userId;
            rental.IssuedAt = DateTime.UtcNow;
            rental.Status = "Active";
            _rentalRepo.Add(rental);
        }

        public void ReturnRental(int rentalId, int userId, string? notes, string? condition, bool force = false)
        {
            var rental = _rentalRepo.GetById(rentalId);
            if (rental == null) throw new InvalidOperationException("Rental not found.");
            if (!force && rental.CustomerId != userId) throw new UnauthorizedAccessException("Cannot return others' rentals.");
            rental.ReturnedAt = DateTime.UtcNow;
            rental.Status = "Returned";
            rental.ReturnNotes = notes;
            rental.ReturnCondition = condition;
            var equipment = _equipmentRepo.GetById(rental.EquipmentId);
            if (equipment != null)
            {
                equipment.IsAvailable = true;
                equipment.Status = Equipment.EquipmentStatus.Available;
                _equipmentRepo.Update(equipment);
            }
            _rentalRepo.Update(rental);
        }

        public void ExtendRental(int rentalId, DateTime newDueDate, string reason, int userId)
        {
            var rental = _rentalRepo.GetById(rentalId);
            if (rental == null || rental.CustomerId != userId) throw new InvalidOperationException("Rental not found or unauthorized.");
            if (rental.ReturnedAt != null) throw new InvalidOperationException("Cannot extend a returned rental.");
            rental.DueDate = newDueDate;
            rental.ExtensionReason = reason;
            _rentalRepo.Update(rental);
        }

        public void CancelRental(int rentalId, int userId, bool force = false)
        {
            var rental = _rentalRepo.GetById(rentalId);
            if (rental == null) throw new InvalidOperationException("Rental not found.");
            if (!force && rental.CustomerId != userId) throw new UnauthorizedAccessException("Cannot cancel others' rentals.");
            var equipment = _equipmentRepo.GetById(rental.EquipmentId);
            if (equipment != null)
            {
                equipment.IsAvailable = true;
                equipment.Status = Equipment.EquipmentStatus.Available;
                _equipmentRepo.Update(equipment);
            }
            _rentalRepo.Delete(rental);
        }
    }
}
