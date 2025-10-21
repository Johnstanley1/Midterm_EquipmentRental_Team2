using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;
using static Midterm_EquipmentRental_Team2.Models.Rental;
using static Midterm_EquipmentRental_Team2.Models.Equipment;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public class RentalRepository: IRentalRepository
    {
        private readonly AppDbContext _context;
        public RentalRepository(AppDbContext context) 
        {
            _context = context;
        }


        public IEnumerable<RentalDTO> GetAll()
        {
            return _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Equipment)
                .Select(r => new RentalDTO
                {
                    Id = r.Id,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    ReturnNotes = r.ReturnNotes,
                    Status = r.Status.ToString(),
                    EquipmentCondition = r.EquipmentCondition.ToString(),
                    EquipmentStatus = r.EquipmentStatus.ToString(),
                    CustomerId = r.CustomerId,
                    EquipmentId = r.EquipmentId,
                    CustomerName = r.Customer.Name,
                    EquipmentName = r.Equipment.Name,
                    Equipment = r.Equipment
                })
                .ToList();
        }


        public Rental GetByEntityId(int id)
        {
            return _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Equipment)
                .FirstOrDefault(r => r.Id == id);
        }


        public RentalDTO? GetById(int id)
        {
            return _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Equipment)
                .Select(r => new RentalDTO
                {
                    Id = r.Id,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    ReturnNotes = r.ReturnNotes,
                    Status = r.Status.ToString(),
                    EquipmentCondition = r.EquipmentCondition.ToString(),
                    EquipmentStatus = r.EquipmentStatus.ToString(),
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.Name,
                    EquipmentName = r.Equipment.Name,
                    Equipment = r.Equipment
                })
                .FirstOrDefault(r => r.Id == id);
                
        }

        public IEnumerable<RentalDTO> GetRented(int id)
        {
            return _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Equipment)
                .Where(r => r.EquipmentId == id)
                .OrderBy(r => r.IssuedAt)
                .Select(r => new RentalDTO
                {
                    Id = r.Id,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    ReturnNotes = r.ReturnNotes,
                    Status = r.Status.ToString(),
                    EquipmentCondition = r.EquipmentCondition.ToString(),
                    EquipmentStatus = r.EquipmentStatus.ToString(),
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.Name,
                    EquipmentName = r.Equipment.Name,
                    Equipment = r.Equipment
                })
                .ToList();
        }


        public IEnumerable<Enums> GetStatus()
        {
            return Enum.GetValues(typeof(RentalStatus))
                .Cast<RentalStatus>()
                .Select(c => new Enums { Id = (int)c, Name = c.ToString() })
               .ToList();
        }


        public IEnumerable<RentalDTO> GetActive()
        {
            return _context.Rentals
                .Where(r => r.Status == RentalStatus.Active && r.ReturnedAt == null)
                .Select(r => new RentalDTO
                {
                    Id = r.Id,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    ReturnNotes = r.ReturnNotes,
                    Status = r.Status.ToString(),
                    EquipmentCondition = r.EquipmentCondition.ToString(),
                    EquipmentStatus = r.EquipmentStatus.ToString(),
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.Name,
                    EquipmentName = r.Equipment.Name,
                    Equipment = r.Equipment 
                })
                .ToList();
        }


        public IEnumerable<RentalDTO> GetCompleted()
        {
            return _context.Rentals
                .Where(r => r.Status == RentalStatus.Returned && r.ReturnedAt != null)
                .Select(r => new RentalDTO
                {
                    Id = r.Id,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    ReturnNotes = r.ReturnNotes,
                    Status = r.Status.ToString(),
                    EquipmentCondition = r.EquipmentCondition.ToString(),
                    EquipmentStatus = r.EquipmentStatus.ToString(),
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.Name,
                    EquipmentName = r.Equipment.Name,
                    Equipment = r.Equipment // or map to EquipmentDTO if needed
                })
                .ToList();
        }



        public IEnumerable<RentalDTO> GetOverdue()
        {
            return _context.Rentals
                .Where(r => r.DueDate < DateTime.UtcNow && r.ReturnedAt == null)
                .Select(r => new RentalDTO
                {
                    Id = r.Id,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    ReturnNotes = r.ReturnNotes,
                    Status = r.Status.ToString(),
                    EquipmentCondition = r.EquipmentCondition.ToString(),
                    EquipmentStatus = r.EquipmentStatus.ToString(),
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.Name,
                    EquipmentName = r.Equipment.Name,
                    Equipment = r.Equipment // or map to EquipmentDTO
                })
                .ToList();
        }



        public void Return(Rental rental)
        {
            _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Equipment)
                .Where(r => r.DueDate < DateTime.UtcNow && r.ReturnedAt == null)
                .ToList();
        }

        public void Update(Rental rental)
        {
            _context.Rentals.Update(rental);
        }

        public void Add(Rental rental)
        {
            _context.Rentals.Add(rental);
        }

        public void Delete(Rental rental)
        {
            _context.Rentals.Remove(rental);
        }
    }
}
