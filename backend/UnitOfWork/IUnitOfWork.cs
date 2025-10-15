using Midterm_EquipmentRental_Team2.Repositories;
using Midterm_EquipmentRental_Team2.Services;

/// <summary>
/// This cordinate the Equipment repository and treat it as a single transaction
/// preventing partial updates and ensuring consistency
/// </summary>

namespace Midterm_EquipmentRental_Team2.UnitOfWork
{
    public interface IUnitOfWork
    {
        IEquipementService Equipements { get; }
        ICustomerService Customers { get; }
        IRentalService Rentals { get; }

        int Complete();
    }
}
