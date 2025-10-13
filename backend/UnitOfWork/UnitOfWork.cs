using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Services;

/// <summary>
/// Coordinates the Equipement repository operation. It also provides a single entry point to comit all changes   
/// </summary>
namespace Midterm_EquipmentRental_Team2.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _appDbContext;
        public IEquipementService Equipements { get; }

        public UnitOfWork(AppDbContext appDbContext, IEquipementService equipementService)
        { 
            _appDbContext = appDbContext;
            Equipements = equipementService;
        }

        public int complete()
        {
            return _appDbContext.SaveChanges();
        }
    }
}
