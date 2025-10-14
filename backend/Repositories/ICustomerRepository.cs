using Midterm_EquipmentRental_Team2.Models;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        IEnumerable<Rental> GetCustomerRentals(int customerId);
        Rental? GetActiveRental(int customerId);
    }
}
