using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerDTO> GetAll();
        CustomerDTO GetById(int id);
        Customer GetByEntityId(int id);
        CustomerDTO GetActiveRental(int customerId);
        CustomerDTO GetCustomerRentals(int customerId);
        IEnumerable<Enums> GetRoles();
        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
