using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAllCustomers();
        CustomerDTO GetCustomerById(int id);
        Customer GetByCustomerEntityId(int id);
        CustomerDTO GetCustomerRentalsById(int customerId);
        CustomerDTO GetActiveCustomerRentalsById(int customerId);
        IEnumerable<Enums> GetCustomerRoles();
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
