using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        Customer GetActiveRental(int customerId);
        Customer GetCustomerRentals(int customerId);
        IEnumerable<Enums> GetRoles();
        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        
        //IEnumerable<Customer> GetAllWithGraph();
        //Customer? GetByIdWithGraph(int id);
    }
}
