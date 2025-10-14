using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Repositories;

namespace Midterm_EquipmentRental_Team2.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers() => _customerRepository.GetAllCustomers();

        public Customer GetCustomerById(int id) => _customerRepository.GetCustomerById(id);

        public void CreateCustomer(Customer customer) => _customerRepository.CreateCustomer(customer);

        public void UpdateCustomer(Customer customer) => _customerRepository.UpdateCustomer(customer);

        public void DeleteCustomer(Customer customer) => _customerRepository.DeleteCustomer(customer);

        public IEnumerable<Rental> GetCustomerRentals(int customerId) => _customerRepository.GetCustomerRentals(customerId);

        public Rental? GetActiveRental(int customerId) => _customerRepository.GetActiveRental(customerId);

    }
}
