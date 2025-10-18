using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public interface IRentalRepository
    {

        IEnumerable<Rental> GetAll();
        IEnumerable<Enums> GetStatus();
        IEnumerable<Rental> GetRented(int equipmentId);
        IEnumerable<Rental> GetActive(int equipmentId);
        IEnumerable<Rental> GetCompleted(int equipmentId);
        IEnumerable<Rental> GetOverdue();
        Rental? GetById(int id);
        void Add(Rental rental);
        void Update(Rental rental);
        void Return(Rental rental);
        void Delete(Rental rental);
    }
}
