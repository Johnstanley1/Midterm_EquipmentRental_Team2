using Midterm_EquipmentRental_Team2.Models;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public interface IRentalRepository
    {

        IEnumerable<Rental> GetAll();
        Rental? GetById(int id);
        IEnumerable<Rental> GetByEquipmentId(int equipmentId);
        IEnumerable<Rental> GetByCustomerId(int customerId);
        IEnumerable<Rental> GetActiveByCustomerId(int customerId);
        IEnumerable<Rental> GetCompletedByCustomerId(int customerId);
        IEnumerable<Rental> GetOverdue();
        void Add(Rental rental);
        void Update(Rental rental);
        void Delete(Rental rental);
    }
}
