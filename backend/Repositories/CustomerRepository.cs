using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;
using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental_Team2.Models.DTOs;
using static Midterm_EquipmentRental_Team2.Models.Equipment;
using static Midterm_EquipmentRental_Team2.Models.Customer;
using static Midterm_EquipmentRental_Team2.Models.Rental;


/// <summary>
/// Equipment repository class implementing the Equipment repository interface crud operations
/// </summary>

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private  AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }


        public Customer GetById(int id)
        {
            return _context.Customers.Find(id);
        }


        public Customer GetCustomerRentals(int id)
        {
            var customer = _context.Customers
                .Include(r => r.Rentals)
                .ThenInclude(e => e.Equipments)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null) return null;

            return new Customer 
            { 
                Id = id,
                Name = customer.Name,
                Rentals = customer.Rentals.ToList(),
            };
        }


        public Customer GetActiveRental(int id)
        {
            var customer = _context.Customers
                .Include(r => r.Rentals)
                .ThenInclude(e => e.Equipment)
                .FirstOrDefault(r => r.Id == id);

            if (customer == null) return null;

            // Filter rentals to only active ones
            customer.Rentals = customer.Rentals
                .Where(r => r.Status == RentalStatus.Active)
                .ToList();

            return customer;
        }


        public IEnumerable<Enums> GetRoles()
        {
            return Enum.GetValues(typeof(UserRole))
                .Cast<EquipmentStatus>().
                Select(c => new Enums { Id = (int)c, Name = c.ToString() })
               .ToList();
        }


        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
        }


        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }


        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }


        

        //public IEnumerable<Customer> GetAllWithGraph() =>
        //    _context.Customers
        //        .Include(c => c.Rentals)
        //        .ThenInclude(r => r.Equipment)
        //        .AsNoTracking()
        //        .ToList();

        //public Customer? GetByIdWithGraph(int id) =>
        //    _context.Customers
        //        .Include(c => c.Rentals)
        //        .ThenInclude(r => r.Equipment)
        //        .AsNoTracking()
        //        .FirstOrDefault(c => c.Id == id);

    }
}
