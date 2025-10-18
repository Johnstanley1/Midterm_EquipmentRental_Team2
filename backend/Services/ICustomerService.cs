using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAllCustomers();
        CustomerDTO GetCustomerById(int id);
        CustomerDTO GetCustomerRentalsById(int customerId);
        CustomerDTO GetActiveCustomerRentalsById(int customerId);
        IEnumerable<Enums> GetCustomerRoles();
        void CreateCustomer(CustomerDTO customer);
        void UpdateCustomer(CustomerDTO customer);
        void DeleteCustomer(CustomerDTO customer);
    }
}
