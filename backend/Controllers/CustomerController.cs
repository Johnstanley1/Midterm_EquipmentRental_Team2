using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.UnitOfWork;

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

        // GET: /api/customers
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            return Ok(_unitOfWork.Customers.GetAllCustomers());
        }

        // GET: /api/customers/{id}
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            var customer = _unitOfWork.Customers.GetCustomerById(id);
            if (customer == null)
                return NotFound($"No customer found with id {id}");

            // Users can only access their own data
            if (User.IsInRole("User") && customer.Username != User.Identity?.Name)
                return Forbid();

            return Ok(customer);
        }

        // POST: /api/customers
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Customer> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Customer data is required.");

            _unitOfWork.Customers.CreateCustomer(customer);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        // PUT: /api/customers/{id}
        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public ActionResult<Customer> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            var existingCustomer = _unitOfWork.Customers.GetCustomerById(id);
            if (existingCustomer == null)
                return NotFound($"No customer found with id {id}");

            // Users can only update their own data
            if (User.IsInRole("User") && existingCustomer.Username != User.Identity?.Name)
                return Forbid();

            if (User.IsInRole("Admin"))
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Username = customer.Username;
                existingCustomer.Password = customer.Password;
                existingCustomer.Role = customer.Role;
                existingCustomer.IsActive = customer.IsActive;
            }
            else
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Password = customer.Password;
            }

            _unitOfWork.Customers.UpdateCustomer(existingCustomer);
            _unitOfWork.Complete();
            return Ok(existingCustomer);
        }

        // DELETE: /api/customers/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            var existingCustomer = _unitOfWork.Customers.GetCustomerById(id);
            if (existingCustomer == null)
                return NotFound($"No customer found with id {id}");

            _unitOfWork.Customers.DeleteCustomer(existingCustomer);
            _unitOfWork.Complete();
            return Ok(existingCustomer);
        }

        // GET: /api/customers/{id}/rentals
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}/rentals")]
        public ActionResult<IEnumerable<Rental>> GetCustomerRentals(int id)
        {
            var customer = _unitOfWork.Customers.GetCustomerById(id);
            if (customer == null)
                return NotFound($"No customer found with id {id}");

            if (User.IsInRole("User") && customer.Username != User.Identity?.Name)
                return Forbid();

            return Ok(_unitOfWork.Customers.GetCustomerRentals(id));
        }

        // GET: /api/customers/{id}/active-rental
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}/active-rental")]
        public ActionResult<Rental> GetActiveRental(int id)
        {
            var customer = _unitOfWork.Customers.GetCustomerById(id);
            if (customer == null)
                return NotFound($"No customer found with id {id}");

            if (User.IsInRole("User") && customer.Username != User.Identity?.Name)
                return Forbid();

            var activeRental = _unitOfWork.Customers.GetActiveRental(id);
            if (activeRental == null)
                return NotFound("No active rental found.");

            return Ok(activeRental);
        }

    }
}
