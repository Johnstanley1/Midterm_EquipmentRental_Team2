using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Services
{
    public interface ICustomerService
    {
        IEnumerable<Models.Customer> GetAllCustomers();
        Models.Customer GetCustomerById(int id);
        Models.Customer GetCustomerRentalsById(int customerId);
        Models.Customer GetActiveCustomerRentalsById(int customerId);
        IEnumerable<Enums> GetCustomerRoles();
        void CreateCustomer(Models.Customer customer);
        void UpdateCustomer(Models.Customer customer);
        void DeleteCustomer(Models.Customer customer);
    }
}
