using Midterm_EquipmentRental_Team2.Data;
using Midterm_EquipmentRental_Team2.Repositories;
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
        public ICustomerService Customers { get; }
        public IRentalService Rentals { get; }

        public UnitOfWork(AppDbContext appDbContext, IEquipementService equipementService,ICustomerService customerService,IRentalService rentalService)
        { 
            _appDbContext = appDbContext;
            Equipements = equipementService;
            Customers = customerService;
            Rentals = rentalService;

        }

        public int Complete()
        {
            return _appDbContext.SaveChanges();
        }
    }
}
