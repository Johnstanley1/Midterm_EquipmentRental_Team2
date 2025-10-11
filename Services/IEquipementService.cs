using Midterm_EquipmentRental_Team2.Models;

namespace Midterm_EquipmentRental_Team2.Services
{
    /// <summary>
    /// Defines the crud operations on the Equipment service decoupling the business logic
    /// from the database implementation
    /// </summary>
    public interface IEquipementService
    {
        Equipment GetEquipmentById(int id);
        IEnumerable<Equipment> GetAllEquipments();
        void CreateEquipement (Equipment equipment);
        void UpdateEquipement (Equipment equipment);
        void DeleteEquipement (Equipment equipment);
    }
}
