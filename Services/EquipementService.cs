using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Repositories;

/// <summary>
/// Equipment service class implementing the Equipment service interface crud operations
/// </summary>
namespace Midterm_EquipmentRental_Team2.Services
{
    public class EquipementService : IEquipementService
    {
        private IEquipementRepository _repository;

        public EquipementService(IEquipementRepository repository) 
        {
            _repository = repository;
        }


        public Equipment GetEquipmentById(int id)
        {
           return _repository.GetById(id);
        }

        public IEnumerable<Equipment> GetAllEquipments()
        {
            return _repository.GetAll().ToList();    
        }

        public void CreateEquipement(Equipment equipment)
        {
            _repository.Add(equipment);
        }

        public void DeleteEquipement(Equipment equipment)
        {
            _repository.Delete(equipment);
        }

        public void UpdateEquipement(Equipment equipment)
        {
            _repository.Update(equipment);
        }
    }
}
