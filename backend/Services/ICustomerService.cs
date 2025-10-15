using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        IEnumerable<Rental> GetCustomerRentals(int customerId);
        Rental? GetActiveRental(int customerId);



        IEnumerable<CustomerDto> GetAllCustomersDto();
        CustomerDto? GetCustomerDtoById(int id);
    }
}
