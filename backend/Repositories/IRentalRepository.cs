using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public interface IRentalRepository
    {

        IEnumerable<RentalDTO> GetAll();
        IEnumerable<Enums> GetStatus();
        IEnumerable<RentalDTO> GetRented(int id);
        IEnumerable<RentalDTO> GetActive();
        IEnumerable<RentalDTO> GetCompleted();
        IEnumerable<RentalDTO> GetOverdue();
        RentalDTO? GetById(int id);
        Rental GetByEntityId(int id);
        void Add(Rental rental);
        void Update(Rental rental);
        void Return(Rental rental);
        void Delete(Rental rental);
    }
}
