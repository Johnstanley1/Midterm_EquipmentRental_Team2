using Midterm_EquipmentRental_Team2.Models;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    // set database CRUD logic
    public interface IEquipementRepository
    {
        Equipment GetById(int id);
        IEnumerable<Equipment> GetAll();
        void Add(Equipment equipment);
        void Update(Equipment equipment);
        void Delete(Equipment equipment);
    }
}
