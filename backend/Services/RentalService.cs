//using Midterm_EquipmentRental_Team2.Models;
//using Midterm_EquipmentRental_Team2.Models.DTOs;
//using Midterm_EquipmentRental_Team2.Repositories;
//using Midterm_EquipmentRental_Team2.Services;

//namespace Midterm_EquipmentRental_Team2.Services
//{
//    public class RentalService: IRentalService
//    {
//        private readonly IRentalRepository _rentalRepo;
//        private readonly ICustomerRepository _customerRepo;
//        private readonly IEquipementRepository _equipmentRepo;

//        public RentalService(IRentalRepository rentalRepo, ICustomerRepository customerRepo, IEquipementRepository equipmentRepo)
//        {
//            _rentalRepo = rentalRepo;
//            _customerRepo = customerRepo;
//            _equipmentRepo = equipmentRepo;
//        }

//        public IEnumerable<Rental> GetAllRentals(int? userId = null, bool isAdmin = false)
//        {
//            return isAdmin ? _rentalRepo.GetAll() : _rentalRepo.GetByCustomerId(userId ?? 0);
//        }

//        public IEnumerable<RentalDto> GetAllRentalsDto(int? userId = null, bool isAdmin = false)
//        {
//            var list = GetAllRentals(userId, isAdmin);
//            return list.Select(MapToDto).ToList();
//        }

//        public Rental? GetRental(int id, int? userId = null, bool isAdmin = false)
//        {
//            var rental = _rentalRepo.GetById(id);
//            if (rental == null) return null;
//            if (!isAdmin && rental.CustomerId != userId) return null;
//            return rental;
//        }

//        public RentalDto? GetRentalDto(int id, int? userId = null, bool isAdmin = false)
//        {
//            var r = GetRental(id, userId, isAdmin);
//            return r == null ? null : MapToDto(r);
//        }

//        public IEnumerable<Rental> GetRentalsByEquipment(int equipmentId) => _rentalRepo.GetByEquipmentId(equipmentId);

//        public IEnumerable<Rental> GetActiveRentals(int? userId = null, bool isAdmin = false)
//        {
//            return isAdmin ? _rentalRepo.GetAll().Where(r => r.ReturnedAt == null) : _rentalRepo.GetActiveByCustomerId(userId ?? 0);
//        }

//        public IEnumerable<Rental> GetCompletedRentals(int? userId = null, bool isAdmin = false)
//        {
//            return isAdmin ? _rentalRepo.GetAll().Where(r => r.ReturnedAt != null) : _rentalRepo.GetCompletedByCustomerId(userId ?? 0);
//        }

//        public IEnumerable<Rental> GetOverdueRentals() => _rentalRepo.GetOverdue();

//        public void IssueRental(Rental rental, int userId)
//        {
//            var equipment = _equipmentRepo.GetById(rental.EquipmentId);
//            if (equipment == null || !equipment.IsAvailable)
//                throw new InvalidOperationException("Equipment not available.");
//            equipment.IsAvailable = false;
//            equipment.Status = Equipment.EquipmentStatus.Rented;
//            _equipmentRepo.Update(equipment);
//            rental.CustomerId = userId;
//            rental.IssuedAt = rental.IssuedAt == default(DateTime) ? DateTime.UtcNow : rental.IssuedAt;
//            rental.Status = ComputeStatus(rental);
//            _rentalRepo.Add(rental);
//        }

//        public void ReturnRental(int rentalId, int userId, string? notes, string? condition, bool force = false)
//        {
//            var rental = _rentalRepo.GetById(rentalId);
//            if (rental == null) throw new InvalidOperationException("Rental not found.");
//            if (!force && rental.CustomerId != userId) throw new UnauthorizedAccessException("Cannot return others' rentals.");
//            rental.ReturnedAt = DateTime.UtcNow;
//            rental.Status = "Returned";
//            rental.ReturnNotes = notes;

//            // Convert string to enum if not null
//            if (!string.IsNullOrEmpty(condition) && Enum.TryParse<Equipment.EquipmentCondition>(condition, out var condEnum))
//                rental.ReturnCondition = condEnum;
//            else
//                rental.ReturnCondition = null;

//            var equipment = _equipmentRepo.GetById(rental.EquipmentId);
//            if (equipment != null)
//            {
//                equipment.IsAvailable = true;
//                equipment.Status = Equipment.EquipmentStatus.Available;
//                _equipmentRepo.Update(equipment);
//            }
//            _rentalRepo.Update(rental);
//        }

//        public void ExtendRental(int rentalId, DateTime newDueDate, string reason, int userId, bool isAdmin = false)
//        {
//            var rental = _rentalRepo.GetById(rentalId);
//            if (rental == null) throw new InvalidOperationException("Rental not found.");
//            if (!isAdmin && rental.CustomerId != userId) throw new InvalidOperationException("Unauthorized to extend this rental.");
//            if (rental.ReturnedAt != null) throw new InvalidOperationException("Cannot extend a returned rental.");
//            rental.DueDate = newDueDate;
//            rental.ReturnNotes = reason;
//            rental.Status = ComputeStatus(rental);
//            _rentalRepo.Update(rental);
//        }

//        public void CancelRental(int rentalId, int userId, bool force = false)
//        {
//            var rental = _rentalRepo.GetById(rentalId);
//            if (rental == null) throw new InvalidOperationException("Rental not found.");
//            if (!force && rental.CustomerId != userId) throw new UnauthorizedAccessException("Cannot cancel others' rentals.");
//            var equipment = _equipmentRepo.GetById(rental.EquipmentId);
//            if (equipment != null)
//            {
//                equipment.IsAvailable = true;
//                equipment.Status = Equipment.EquipmentStatus.Available;
//                _equipmentRepo.Update(equipment);
//            }
//            _rentalRepo.Delete(rental);
//        }

//        private static RentalDto MapToDto(Rental r) => new RentalDto
//        {
//            Id = r.Id,
//            EquipmentId = r.EquipmentId,
//            EquipmentName = r.Equipment?.Name ?? string.Empty,
//            EquipmentStatus = r.Equipment?.Status.ToString() ?? string.Empty,
//            CustomerId = r.CustomerId,
//            CustomerName = r.Customer?.Name ?? string.Empty,
//            IssuedAt = r.IssuedAt,
//            DueDate = r.DueDate,
//            ReturnedAt = r.ReturnedAt,
//            Status = r.Status
//        };

//        private static string ComputeStatus(Rental r)
//        {
//            if (r.ReturnedAt != null) return "Returned";
//            if (r.DueDate.HasValue && r.DueDate.Value < DateTime.UtcNow) return "Overdue";
//            return "Active";
//        }
//    }
//}
