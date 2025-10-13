using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;
using static Midterm_EquipmentRental_Team2.Models.Equipment;

/// <summary>
/// Equipment repository class implementing the Equipment repository interface crud operations
/// </summary>

namespace Midterm_EquipmentRental_Team2.Repositories
{
    public class EquipementRepository : IEquipementRepository
    {
        private AppDbContext _appDbContext;

        public EquipementRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Equipment> GetAll()
        {
            return _appDbContext.Equipments.ToList();
        }

        public Equipment GetById(int id)
        {
            return _appDbContext.Equipments.Find(id);
        }

        public void Add(Equipment equipment)
        {
            _appDbContext.Equipments.Add(equipment);
        }

        public void Delete(Equipment equipment)
        {
            _appDbContext.Equipments.Remove(equipment);
        }

        public void Update(Equipment equipment)
        {
            _appDbContext.Equipments.Update(equipment);
        }

        public IEnumerable<Equipment> GetAvailable()
        {
            return _appDbContext.Equipments
                .Where(e => e.Status == EquipmentStatus.Available)
                .ToList();
        }

        public IEnumerable<Equipment> GetRented()
        {
            return _appDbContext.Equipments
                .Where(e => e.Status == EquipmentStatus.Rented)
                .ToList();
        }
    }
}
