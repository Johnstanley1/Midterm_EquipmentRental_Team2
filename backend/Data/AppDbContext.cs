using Midterm_EquipmentRental_Team2.Models;
using Microsoft.EntityFrameworkCore;

namespace Midterm_EquipmentRental_Team2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        // seed data into database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "admin", Role = "Admin", IsActive = true }, // admin
                new User { Id = 2, Username = "user1", Password = "user1", Role = "User", IsActive = true },
                new User { Id = 3, Username = "user2", Password = "user2", Role = "User", IsActive = true },
                new User { Id = 4, Username = "user3", Password = "user3", Role = "User", IsActive = true },
                new User { Id = 5, Username = "user4", Password = "user4", Role = "User", IsActive = true },
                new User { Id = 6, Username = "user5", Password = "user5", Role = "User", IsActive = true },
                new User { Id = 7, Username = "alex", Password = "alex", Role = "User", IsActive = true },
                new User { Id = 8, Username = "maria", Password = "maria", Role = "User", IsActive = true }
            );

            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    Id = 1,
                    Name = "Excavator",
                    Category = Equipment.EquipmentCategory.Vehicles,
                    Condition = Equipment.EquipmentCondition.Excellent,
                    Description = "CAT 320 Excavator",
                    IsAvailable = false,
                    Status = Equipment.EquipmentStatus.Rented
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
                    IsAvailable = false,
                    Status = Equipment.EquipmentStatus.Rented
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
                },
                new Equipment
                {
                    Id = 6,
                    Name = "Concrete Mixer",
                    Category = Equipment.EquipmentCategory.HeavyMachinery,
                    Condition = Equipment.EquipmentCondition.Good,
                    Description = "Portable Electric Mixer",
                    IsAvailable = true,
                    Status = Equipment.EquipmentStatus.Available
                },
                new Equipment
                {
                    Id = 7,
                    Name = "Safety Helmet",
                    Category = Equipment.EquipmentCategory.Safety,
                    Condition = Equipment.EquipmentCondition.Excellent,
                    Description = "ANSI Z89.1 Helmet",
                    IsAvailable = true,
                    Status = Equipment.EquipmentStatus.Available
                },
                new Equipment
                {
                    Id = 8,
                    Name = "Laser Level",
                    Category = Equipment.EquipmentCategory.Surveying,
                    Condition = Equipment.EquipmentCondition.Good,
                    Description = "Rotary Laser Level",
                    IsAvailable = false,
                    Status = Equipment.EquipmentStatus.Rented
                },
                new Equipment
                {
                    Id = 9,
                    Name = "Pickup Truck",
                    Category = Equipment.EquipmentCategory.Vehicles,
                    Condition = Equipment.EquipmentCondition.Fair,
                    Description = "1/2 Ton Truck",
                    IsAvailable = true,
                    Status = Equipment.EquipmentStatus.Available
                },
                new Equipment
                {
                    Id = 10,
                    Name = "Chainsaw",
                    Category = Equipment.EquipmentCategory.PowerTools,
                    Condition = Equipment.EquipmentCondition.Good,
                    Description = "Stihl Chainsaw",
                    IsAvailable = true,
                    Status = Equipment.EquipmentStatus.Available
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
                },
                new Customer
                {
                    Id = 4,
                    Name = "Alex Johnson",
                    Username = "alex",
                    Password = "alex",
                    Role = "User",
                    IsActive = true
                },
                new Customer
                {
                    Id = 5,
                    Name = "Maria Garcia",
                    Username = "maria",
                    Password = "maria",
                    Role = "User",
                    IsActive = true
                },
                new Customer
                {
                    Id = 6,
                    Name = "User One",
                    Username = "user1",
                    Password = "user1",
                    Role = "User",
                    IsActive = true
                },
                new Customer
                {
                    Id = 7,
                    Name = "User Two",
                    Username = "user2",
                    Password = "user2",
                    Role = "User",
                    IsActive = true
                },
                new Customer
                {
                    Id = 8,
                    Name = "User Three",
                    Username = "user3",
                    Password = "user3",
                    Role = "User",
                    IsActive = true
                }
            );


            // Seed data for Rental
            modelBuilder.Entity<Rental>().HasData(
                new Rental
                {
                    Id = 1,
                    CustomerId = 1, // Admin User
                    EquipmentId = 1, // Excavator
                    IssuedAt = DateTime.UtcNow.AddDays(-10),
                    DueDate = DateTime.UtcNow.AddDays(10),
                    ReturnedAt = null,
                    Status = "Active"
                },
                new Rental
                {
                    Id = 2,
                    CustomerId = 2, // John Doe
                    EquipmentId = 3, // Surveying Drone
                    IssuedAt = DateTime.UtcNow.AddDays(-5),
                    DueDate = DateTime.UtcNow.AddDays(5),
                    ReturnedAt = null,
                    Status = "Active"
                },
                new Rental
                {
                    Id = 3,
                    CustomerId = 3, // Jane Smith
                    EquipmentId = 4, // Paint Roller
                    IssuedAt = DateTime.UtcNow.AddDays(-20),
                    DueDate = DateTime.UtcNow.AddDays(-10),
                    ReturnedAt = DateTime.UtcNow.AddDays(-9),
                    Status = "Returned",
                    ReturnNotes = "Returned in good condition",
                    ReturnCondition = Equipment.EquipmentCondition.Good

                },
                new Rental
                {
                    Id = 4,
                    CustomerId = 4, // Alex Johnson
                    EquipmentId = 8, // Laser Level
                    IssuedAt = DateTime.UtcNow.AddDays(-2),
                    DueDate = DateTime.UtcNow.AddDays(12),
                    ReturnedAt = null,
                    Status = "Active"
                },
                new Rental
                {
                    Id = 5,
                    CustomerId = 5, // Maria Garcia
                    EquipmentId = 6, // Concrete Mixer
                    IssuedAt = DateTime.UtcNow.AddDays(-30),
                    DueDate = DateTime.UtcNow.AddDays(-5),
                    ReturnedAt = null,
                    Status = "Overdue"
                },
                new Rental
                {
                    Id = 6,
                    CustomerId = 6, // user1
                    EquipmentId = 7, // Safety Helmet
                    IssuedAt = DateTime.UtcNow.AddDays(-1),
                    DueDate = DateTime.UtcNow.AddDays(6),
                    ReturnedAt = null,
                    Status = "Active"
                },
                new Rental
                {
                    Id = 7,
                    CustomerId = 7, // user2
                    EquipmentId = 10, // Chainsaw
                    IssuedAt = DateTime.UtcNow.AddDays(-15),
                    DueDate = DateTime.UtcNow.AddDays(-1),
                    ReturnedAt = DateTime.UtcNow.AddDays(-1),
                    Status = "Returned",
                    ReturnNotes = "Chain replaced",
                    ReturnCondition = Equipment.EquipmentCondition.Fair
                }
            );

        }
    }

    }

