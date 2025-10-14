using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public class RentalRepository: IRentalRepository
    {
        private readonly AppDbContext _context;
        public RentalRepository(AppDbContext context) => _context = context;

        public IEnumerable<Rental> GetAll() =>
            _context.Rentals.Include(r => r.Customer).Include(r => r.Equipment).ToList();

        public Rental? GetById(int id) =>
            _context.Rentals.Include(r => r.Customer).Include(r => r.Equipment).FirstOrDefault(r => r.Id == id);

        public IEnumerable<Rental> GetByEquipmentId(int equipmentId) =>
            _context.Rentals.Include(r => r.Customer).Include(r => r.Equipment)
                .Where(r => r.EquipmentId == equipmentId).ToList();

        public IEnumerable<Rental> GetByCustomerId(int customerId) =>
            _context.Rentals.Include(r => r.Customer).Include(r => r.Equipment)
                .Where(r => r.CustomerId == customerId).ToList();

        public IEnumerable<Rental> GetActiveByCustomerId(int customerId) =>
            _context.Rentals.Include(r => r.Customer).Include(r => r.Equipment)
                .Where(r => r.CustomerId == customerId && r.ReturnedAt == null).ToList();

        public IEnumerable<Rental> GetCompletedByCustomerId(int customerId) =>
            _context.Rentals.Include(r => r.Customer).Include(r => r.Equipment)
                .Where(r => r.CustomerId == customerId && r.ReturnedAt != null).ToList();

        public IEnumerable<Rental> GetOverdue() =>
            _context.Rentals.Include(r => r.Customer).Include(r => r.Equipment)
                .Where(r => r.DueDate < DateTime.UtcNow && r.ReturnedAt == null).ToList();

        public void Add(Rental rental)
        {
            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }

        public void Update(Rental rental)
        {
            _context.Rentals.Update(rental);
            _context.SaveChanges();
        }

        public void Delete(Rental rental)
        {
            _context.Rentals.Remove(rental);
            _context.SaveChanges();
        }
    }
}
