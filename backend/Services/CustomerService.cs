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

        public IEnumerable<Models.Customer> GetAllCustomers() 
        {
            return _customerRepository.GetAll();
        }

        public Models.Customer GetCustomerById(int id) 
        {
            return _customerRepository.GetById(id);
        }


        public Models.Customer GetCustomerRentalsById(int customerId)
        {
            return _customerRepository.GetCustomerRentals(customerId);
        }

        public Models.Customer GetActiveCustomerRentalsById(int customerId)
        {
            return _customerRepository.GetActiveRental(customerId);
        }


        public IEnumerable<Enums> GetCustomerRoles()
        {
            return _customerRepository.GetRoles();
        }


        public void CreateCustomer(Models.Customer customer) 
        { 
            _customerRepository.Create(customer);
        }


        public void UpdateCustomer(Models.Customer customer) 
        {
            _customerRepository.Update(customer);
        }


        public void DeleteCustomer(Models.Customer customer) 
        {
            _customerRepository.Delete(customer);
        }
    }
}
