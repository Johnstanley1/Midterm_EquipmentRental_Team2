using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerDTO> GetAll();
        CustomerDTO GetById(int id);
        CustomerDTO GetActiveRental(int customerId);
        CustomerDTO GetCustomerRentals(int customerId);
        IEnumerable<Enums> GetRoles();
        void Create(CustomerDTO customer);
        void Update(CustomerDTO customer);
        void Delete(CustomerDTO customer);
    }
}
