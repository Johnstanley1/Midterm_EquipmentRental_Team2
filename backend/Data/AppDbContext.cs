using Midterm_EquipmentRental_Team2.Models;
using Microsoft.EntityFrameworkCore;

namespace Midterm_EquipmentRental_Team2.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Rental> Rentals { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
     

        // seed data into database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Username = "admin", Password = "admin", Role = "Admin", IsActive = true }, // admin
                    new User { Id = 2, Username = "user1", Password = "user1", Role = "User1", IsActive = true },
                    new User { Id = 3, Username = "user2", Password = "user2", Role = "User2", IsActive = true },
                    new User { Id = 4, Username = "user3", Password = "user3", Role = "User3", IsActive = true },
                    new User { Id = 5, Username = "user4", Password = "user4", Role = "User4", IsActive = true },
                    new User { Id = 6, Username = "user5", Password = "user5", Role = "User5", IsActive = true }
             );

            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    Id = 1,
                    Name = "Excavator",
                    Category = Equipment.EquipmentCategory.Vehicles,
                    Condition = Equipment.EquipmentCondition.Excellent,
                    Description = "CAT 320 Excavator",
                    IsAvailable = true,
                    Status = Equipment.EquipmentStatus.Available
                },
                new Equipment
                {
                    Id = 2,
                    Name = "Jackhammer",
                    Category = Equipment.EquipmentCategory.PowerTools,
                    Condition = Equipment.EquipmentCondition.Excellent,
                    Description = "Bosch Electric Jackhammer",
                    IsAvailable = false,
                    Status = Equipment.EquipmentStatus.Maintenance
                },
                new Equipment
                {
                    Id = 3,
                    Name = "Surveying Drone",
                    Category = Equipment.EquipmentCategory.HeavyMachinery,
                    Condition = Equipment.EquipmentCondition.Good,
                    Description = "DJI Phantom 4 RTK",
                    IsAvailable = true,
                    Status = Equipment.EquipmentStatus.Available
                },
                new Equipment
                {
                    Id = 4,
                    Name = "Paint Roller",
                    Category = Equipment.EquipmentCategory.PowerTools,
                    Condition = Equipment.EquipmentCondition.Excellent,
                    Description = "Double Sided Roller",
                    IsAvailable = false,
                    Status = Equipment.EquipmentStatus.Rented
                },
                new Equipment
                {
                    Id = 5,
                    Name = "Mechnical Gloves",
                    Category = Equipment.EquipmentCategory.Safety,
                    Condition = Equipment.EquipmentCondition.New,
                    Description = "Rechargeable Gloves",
                    IsAvailable = true,
                    Status = Equipment.EquipmentStatus.Maintenance
                }
            );


            // Relationships
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Equipment)
                .WithMany()
                .HasForeignKey(r => r.EquipmentId);

            // Seed data for Customer
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Admin User",
                    Username = "admin",
                    Password = "admin123", // For demo only; use hashing in production
                    Role = "Admin",
                    IsActive = true
                },
                new Customer
                {
                    Id = 2,
                    Name = "John Doe",
                    Username = "john",
                    Password = "john123",
                    Role = "User",
                    IsActive = true
                },
                new Customer
                {
                    Id = 3,
                    Name = "Jane Smith",
                    Username = "jane",
                    Password = "jane123",
                    Role = "User",
                    IsActive = true
                }
            );



            // Rentals can be seeded if needed, but typically start empty
        }

    }
}
