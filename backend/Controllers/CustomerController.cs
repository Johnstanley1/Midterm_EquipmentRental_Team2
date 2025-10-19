using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.UnitOfWork;
using Midterm_EquipmentRental_Team2.Models.DTOs;


/// <summary>
/// API Controller for managing Customer entity.
/// Provides full CRUD operations to Create, Read, Update, and Delete products.
/// Uses services and Unit of Work patterns for data access.
/// </summary>
namespace Midterm_EquipmentRental_Team2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            var customers = _unitOfWork.Customers.GetAllCustomers();
            return Ok(customers);
        }


        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetCustomerById(int id)
        {
            var customer = _unitOfWork.Customers.GetCustomerById(id);
            if (customer == null)
                return NotFound($"No customer found with id {id}");

            
            if (User.IsInRole("User") && customer.Username != User.Identity?.Name) // Users can only access their own data
                return Forbid();

            return Ok(customer);
        }


        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}/rentals")]
        public ActionResult<CustomerDTO> GetCustomerRentalsById(int id)
        {
            var customer = _unitOfWork.Customers.GetCustomerRentalsById(id);
            if (customer == null)
                return NotFound($"No customer found with id {id}");

            if (User.IsInRole("User") && customer.Username != User.Identity?.Name) // Users can only access their own data
                return Forbid();

            return Ok(customer);
        }


        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}/active-rental")]
        public ActionResult<CustomerDTO> GetActiveCustomerRentalsById(int id)
        {
            var customer = _unitOfWork.Customers.GetActiveCustomerRentalsById(id);
            if (customer == null)
                return NotFound($"No customer found with id {id}");

            if (User.IsInRole("User") && customer.Username != User.Identity?.Name) // Users can only access their own data
                return Forbid();

            return Ok(customer);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Customer> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Customer data is required.");

            customer.Role = Enum.Parse<Customer.UserRole>(customer.Role.ToString(), true);

            _unitOfWork.Customers.CreateCustomer(customer);
            _unitOfWork.Complete();
            return Ok(customer);
        }



        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public ActionResult<Customer> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            var existingCustomer = _unitOfWork.Customers.GetByCustomerEntityId(id);
            if (existingCustomer == null)
                return NotFound($"No customer found with id {id}");


            if (User.IsInRole("User") && existingCustomer.Username != User.Identity?.Name) // Users can only access their own data
                return Forbid();

            if (User.IsInRole("Admin"))
            {
                existingCustomer.Id = id;
                existingCustomer.Name = customer.Name;
                existingCustomer.Username = customer.Username;
                existingCustomer.Password = customer.Password;
                existingCustomer.IsActive = customer.IsActive;
                existingCustomer.Role = customer.Role;
                existingCustomer.IsActive = customer.IsActive;
                existingCustomer.Rentals = customer.Rentals;
            }
            else
            {
                existingCustomer.Id = id;
                existingCustomer.Name = customer.Name;
                existingCustomer.Username = customer.Username;
                existingCustomer.Password = customer.Password;
                existingCustomer.IsActive = customer.IsActive;
                existingCustomer.IsActive = customer.IsActive;
                existingCustomer.Rentals = customer.Rentals;
            }

            _unitOfWork.Customers.UpdateCustomer(existingCustomer);
            _unitOfWork.Complete();
            return Ok(existingCustomer);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            var existingCustomer = _unitOfWork.Customers.GetByCustomerEntityId(id);
            if (existingCustomer == null)
                return NotFound($"No customer found with id {id}");

            _unitOfWork.Customers.DeleteCustomer(existingCustomer);
            _unitOfWork.Complete();

            var resultDto = new CustomerDTO
            {
                Id = existingCustomer.Id,
                Name = existingCustomer.Name,
                Username = existingCustomer.Username,
                Role = existingCustomer.Role.ToString(),
                IsActive = existingCustomer.IsActive,
                Rentals = existingCustomer.Rentals.Select(r => new RentalDTO
                {
                    Id = r.Id,
                    IssuedAt = r.IssuedAt,
                    DueDate = r.DueDate,
                    ReturnedAt = r.ReturnedAt,
                    ReturnNotes = r.ReturnNotes,
                    EquipmentStatus = r.EquipmentStatus.ToString(),
                    EquipmentCondition = r.EquipmentCondition.ToString(),
                    Status = r.Status.ToString(),
                    CustomerId = r.CustomerId,
                    EquipmentId = r.EquipmentId,
                    Equipment = r.Equipment
                }).ToList()
            };

            return Ok(existingCustomer);
        }
    }
}
