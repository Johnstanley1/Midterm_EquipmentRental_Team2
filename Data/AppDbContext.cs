using Midterm_EquipmentRental_Team2.Models;
using System.Collections.Generic;

namespace Midterm_EquipmentRental_Team2.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }

    }
}
