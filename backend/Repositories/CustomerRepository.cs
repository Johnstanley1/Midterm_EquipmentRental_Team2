using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;
using Microsoft.EntityFrameworkCore;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private  AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.Include(c => c.Rentals).ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.Include(c => c.Rentals).FirstOrDefault(c => c.Id == id);
        }

        public void CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public IEnumerable<Rental> GetCustomerRentals(int customerId)
        {
            return _context.Rentals.Where(r => r.CustomerId == customerId).ToList();
        }

        public Rental? GetActiveRental(int customerId)
        {
            return _context.Rentals
                .Where(r => r.CustomerId == customerId && r.Status == "Active")
                .FirstOrDefault();
        }
    }
}
