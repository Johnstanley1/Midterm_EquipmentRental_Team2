using Midterm_EquipmentRental_Team2.Models;
using static Midterm_EquipmentRental_Team2.Models.Equipment;

namespace Midterm_EquipmentRental_Team2.Repositories
{
    /// <summary>
    /// Defines the crud operations on the Equipment model decoupling the business logic
    /// from the database implementation
    /// </summary>
    public interface IEquipementRepository
    {
        Equipment GetById(int id);
        IEnumerable<Equipment> GetAll();
        IEnumerable<Equipment> GetAvailable();
        IEnumerable<Equipment> GetRented();
        void Add(Equipment equipment);
        void Update(Equipment equipment);
        void Delete(Equipment equipment);
    }
}
