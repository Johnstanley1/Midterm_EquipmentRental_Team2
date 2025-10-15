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

        public IEnumerable<Customer> GetAllCustomers() => _customerRepository.GetAllCustomers();

        public Customer GetCustomerById(int id) => _customerRepository.GetCustomerById(id);

        public void CreateCustomer(Customer customer) => _customerRepository.CreateCustomer(customer);

        public void UpdateCustomer(Customer customer) => _customerRepository.UpdateCustomer(customer);

        public void DeleteCustomer(Customer customer) => _customerRepository.DeleteCustomer(customer);

    public IEnumerable<Rental> GetCustomerRentals(int customerId) => _customerRepository.GetCustomerRentals(customerId);

    public Rental? GetActiveRental(int customerId) => _customerRepository.GetActiveRental(customerId);

        public IEnumerable<CustomerDto> GetAllCustomersDto()
        {
            var customers = _customerRepository.GetAllWithGraph();
            return customers.Select(MapCustomerToDto).ToList();
        }

        public CustomerDto? GetCustomerDtoById(int id)
        {
            var c = _customerRepository.GetByIdWithGraph(id);
            return c == null ? null : MapCustomerToDto(c);
        }

        private static CustomerDto MapCustomerToDto(Customer c) =>
            new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Username = c.Username,
                Role = c.Role,
                IsActive = c.IsActive,
                Rentals = c.Rentals?
                    .OrderByDescending(r => r.IssuedAt)
                    .Select(r => new RentalSummaryDto
                    {
                        Id = r.Id,
                        EquipmentId = r.EquipmentId,
                        EquipmentName = r.Equipment?.Name ?? string.Empty,
                        EquipmentStatus = r.Equipment?.Status.ToString() ?? string.Empty,
                        IssuedAt = r.IssuedAt,
                        DueDate = r.DueDate,
                        ReturnedAt = r.ReturnedAt,
                        Status = r.Status
                    }).ToList() ?? new List<RentalSummaryDto>()
            };

    }
}
