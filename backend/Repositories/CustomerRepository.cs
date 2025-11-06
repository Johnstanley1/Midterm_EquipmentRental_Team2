using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;
using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental_Team2.Models.DTOs;
using static Midterm_EquipmentRental_Team2.Models.Equipment;
using static Midterm_EquipmentRental_Team2.Models.Customer;
using static Midterm_EquipmentRental_Team2.Models.Rental;


/// <summary>
/// Equipment repository class implementing the Equipment repository interface crud operations
/// </summary>

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private  AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<CustomerDTO> GetAll()
        {
            return _context.Customers
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Username = c.Username,
                    Email = c.Email,
                    Password = c.Password,
                    Role = c.Role.ToString(),
                    IsActive = c.IsActive
                })
                .ToList();
        }

        public Customer GetByEntityId(int id)
        {
            return _context.Customers
            .Include(c => c.Rentals)  
            .ThenInclude(c => c.Equipment)
            .FirstOrDefault(c => c.Id == id);
        }


        public CustomerDTO GetById(int id)
        {
            var customer = _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Username = c.Username,
                    Email = c.Email,
                    Password = c.Password,
                    Role = c.Role.ToString(),
                    IsActive = c.IsActive
                })
                .FirstOrDefault();

            return customer;
        }



        public CustomerDTO GetCustomerRentals(int id)
        {
            var customer = _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Username = c.Username,
                    Email = c.Email,
                    Role = c.Role.ToString(),
                    IsActive = c.IsActive,
                    Rentals = c.Rentals.Select(r => new RentalDTO
                    {
                        Id = r.Id,
                        IssuedAt = r.IssuedAt,
                        DueDate = r.DueDate,
                        ReturnedAt = r.ReturnedAt,
                        Status = r.Status.ToString(),
                        EquipmentCondition = r.EquipmentCondition.ToString(),
                        EquipmentStatus = r.EquipmentStatus.ToString(),
                        EquipmentName = r.Equipment.Name,
                        CustomerId = r.CustomerId,
                        EquipmentId = r.EquipmentId,
                        CustomerName = r.Customer.Name,
                        Equipment = r.Equipment
                    }).ToList()
                })
                .FirstOrDefault();

            return customer;
        }



        public CustomerDTO GetActiveRental(int id)
        {
            var customer = _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Username = c.Username,
                    Email = c.Email,
                    Role = c.Role.ToString(),
                    IsActive = c.IsActive,
                    Rentals = c.Rentals
                        .Where(r => r.Status == RentalStatus.Active)
                        .Select(r => new RentalDTO
                        {
                            Id = r.Id,
                            IssuedAt = r.IssuedAt,
                            DueDate = r.DueDate,
                            ReturnedAt = r.ReturnedAt,
                            Status = r.Status.ToString(),
                            EquipmentCondition = r.EquipmentCondition.ToString(),
                            EquipmentStatus = r.EquipmentStatus.ToString(),
                            EquipmentName = r.Equipment.Name,
                            CustomerId = r.CustomerId,
                            EquipmentId = r.EquipmentId,
                            CustomerName = r.Customer.Name,
                            Equipment = r.Equipment
                        }).ToList()
                })
                .FirstOrDefault();

            return customer;
        }



        public IEnumerable<Enums> GetRoles()
        {
            return Enum.GetValues(typeof(UserRole))
                .Cast<EquipmentStatus>().
                Select(c => new Enums { Id = (int)c, Name = c.ToString() })
               .ToList();
        }


        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
        }


        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }


        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

    }
}
