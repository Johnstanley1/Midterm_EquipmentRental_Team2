using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Models;
using Midterm_EquipmentRental_Team2.Models.DTOs;
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

        public IEnumerable<Enums> GetStatus()
        {
            return Enum.GetValues(typeof(EquipmentStatus))
                .Cast<EquipmentStatus>().
                Select(c => new Enums { Id = (int)c, Name = c.ToString() })
               .ToList();
        }

        public IEnumerable<Enums> GetCondition()
        {
            return Enum.GetValues(typeof(EquipmentCondition))
                .Cast<EquipmentCondition>().
                Select(c => new Enums { Id = (int)c, Name = c.ToString() })
               .ToList();
        }

        public IEnumerable<Enums> GetCategory()
        {
            return Enum.GetValues(typeof(EquipmentCategory))
                .Cast<EquipmentCategory>().
                Select(c => new Enums { Id = (int)c, Name = c.ToString() })
               .ToList();
        }
    }
}
