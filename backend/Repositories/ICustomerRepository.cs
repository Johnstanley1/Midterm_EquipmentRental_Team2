using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Models.Customer> GetAll();
        Models.Customer GetById(int id);
        Models.Customer GetActiveRental(int customerId);
        Models.Customer GetCustomerRentals(int customerId);
        IEnumerable<Enums> GetRoles();
        void Create(Models.Customer customer);
        void Update(Models.Customer customer);
        void Delete(Models.Customer customer);
        
        //IEnumerable<Customer> GetAllWithGraph();
        //Customer? GetByIdWithGraph(int id);
    }
}
