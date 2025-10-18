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


        public IEnumerable<Rental> GetAll()
        {
            return _context.Rentals
                .Include(c => c.Customer)
                .Include(e => e.Equipments)
                .ToList();
        }


        public Rental? GetById(int id)
        {
            return _context.Rentals
                .Include(c => c.Customer)
                .Include(e => e.Equipments)
                .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Rental> GetRented(int id)
        {
            return _context.Rentals
                .Include(c => c.Customer)
                .Include(e => e.Equipments)
                .Where(r => r.EquipmentStatus == EquipmentStatus.Rented)
                .ToList();
        }

        public IEnumerable<Enums> GetStatus()
        {
            return Enum.GetValues(typeof(RentalStatus))
                .Cast<RentalStatus>()
                .Select(c => new Enums { Id = (int)c, Name = c.ToString() })
               .ToList();
        }


        public IEnumerable<Rental> GetActive(int id) 
        {
            return _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Equipments)
                .Where(r => r.CustomerId == id && r.ReturnedAt == null)
                .ToList();

        }

        public IEnumerable<Rental> GetCompleted(int id)
        {
            return _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Equipments)
                .Where(r => r.CustomerId == id && r.ReturnedAt != null).ToList();
        }
        

        public IEnumerable<Rental> GetOverdue()
        {
            return _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Equipments)
                .Where(r => r.DueDate < DateTime.UtcNow && r.ReturnedAt == null)
                .ToList();
        }
        

        public void Return(Rental rental)
        {
            _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Equipments)
                .Where(r => r.DueDate < DateTime.UtcNow && r.ReturnedAt == null)
                .ToList();
        }

        public void Update(Rental rental)
        {
            _context.Rentals.Update(rental);
        }

        public void Add(Rental rental)
        {
            _context.Add(rental);
        }

        public void Delete(Rental rental)
        {
            _context.Rentals.Remove(rental);
        }
    }
}
