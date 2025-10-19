using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;
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

        public IEnumerable<CustomerDTO> GetAllCustomers() 
        {
            return _customerRepository.GetAll();
        }

        public Customer GetByCustomerEntityId(int id) 
        {
            return _customerRepository.GetByEntityId(id);
        }


        public CustomerDTO GetCustomerById(int id) 
        {
            return _customerRepository.GetById(id);
        }


        public CustomerDTO GetCustomerRentalsById(int customerId)
        {
            return _customerRepository.GetCustomerRentals(customerId);
        }

        public CustomerDTO GetActiveCustomerRentalsById(int customerId)
        {
            return _customerRepository.GetActiveRental(customerId);
        }


        public IEnumerable<Enums> GetCustomerRoles()
        {
            return _customerRepository.GetRoles();
        }


        public void CreateCustomer(Customer customer) 
        { 
            _customerRepository.Create(customer);
        }


        public void UpdateCustomer(Customer customer) 
        {
            _customerRepository.Update(customer);
        }


        public void DeleteCustomer(Customer customer) 
        {
            _customerRepository.Delete(customer);
        }
    }
}
