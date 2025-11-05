using Midterm_EquipmentRental_Team2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using static Midterm_EquipmentRental_Team2.Models.Equipment;

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
                // new User { Id = 1, Username = "admin", Password = "admin", Role = "Admin", IsActive = true }, // admin
                // new User { Id = 2, Username = "user1", Password = "user1", Role = "User", IsActive = true },
                // new User { Id = 3, Username = "user2", Password = "user2", Role = "User", IsActive = true },
                // new User { Id = 4, Username = "user3", Password = "user3", Role = "User", IsActive = true },
                // new User { Id = 5, Username = "user4", Password = "user4", Role = "User", IsActive = true },
                // new User { Id = 6, Username = "user5", Password = "user5", Role = "User", IsActive = true },
                // new User { Id = 7, Username = "alex", Password = "alex", Role = "User", IsActive = true },
                // new User { Id = 8, Username = "maria", Password = "maria", Role = "User", IsActive = true },
                new User { Id = 1,  Role = "Admin", Email = "johnstanley.ajagu@gmail.com", ExternalProvider = "Google", ExternalId = "", IsActive = true },// admin
                new User { Id = 2,  Role = "User", Email = "mickeywang19920110@gmail.com", ExternalProvider = "Google", ExternalId = "", IsActive = true }
            );

            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    Id = 1,
                    Name = "Excavator",
                    Category = EquipmentCategory.Vehicles,
                    Condition = EquipmentCondition.Excellent,
                    Description = "CAT 320 Excavator",
                    IsAvailable = false,
                    Status = EquipmentStatus.Rented
                },
                new Equipment
                {
                    Id = 2,
                    Name = "Jackhammer",
                    Category = EquipmentCategory.PowerTools,
                    Condition = EquipmentCondition.Excellent,
                    Description = "Bosch Electric Jackhammer",
                    IsAvailable = false,
                    Status = EquipmentStatus.Maintenance
                },
                new Equipment
                {
                    Id = 3,
                    Name = "Surveying Drone",
                    Category = EquipmentCategory.HeavyMachinery,
                    Condition = EquipmentCondition.Good,
                    Description = "DJI Phantom 4 RTK",
                    IsAvailable = false,
                    Status = EquipmentStatus.Rented
                },
                new Equipment
                {
                    Id = 4,
                    Name = "Paint Roller",
                    Category = EquipmentCategory.PowerTools,
                    Condition = EquipmentCondition.Excellent,
                    Description = "Double Sided Roller",
                    IsAvailable = false,
                    Status = EquipmentStatus.Rented
                },
                new Equipment
                {
                    Id = 5,
                    Name = "Mechnical Gloves",
                    Category = EquipmentCategory.Safety,
                    Condition = EquipmentCondition.New,
                    Description = "Rechargeable Gloves",
                    IsAvailable = true,
                    Status = EquipmentStatus.Maintenance
                },
                new Equipment
                {
                    Id = 6,
                    Name = "Concrete Mixer",
                    Category = EquipmentCategory.HeavyMachinery,
                    Condition = EquipmentCondition.Good,
                    Description = "Portable Electric Mixer",
                    IsAvailable = true,
                    Status = EquipmentStatus.Available
                },
                new Equipment
                {
                    Id = 7,
                    Name = "Safety Helmet",
                    Category = EquipmentCategory.Safety,
                    Condition = EquipmentCondition.Excellent,
                    Description = "ANSI Z89.1 Helmet",
                    IsAvailable = true,
                    Status = EquipmentStatus.Available
                },
                new Equipment
                {
                    Id = 8,
                    Name = "Laser Level",
                    Category = EquipmentCategory.Surveying,
                    Condition = EquipmentCondition.Good,
                    Description = "Rotary Laser Level",
                    IsAvailable = false,
                    Status = EquipmentStatus.Rented
                },
                new Equipment
                {
                    Id = 9,
                    Name = "Pickup Truck",
                    Category = EquipmentCategory.Vehicles,
                    Condition = EquipmentCondition.Fair,
                    Description = "1/2 Ton Truck",
                    IsAvailable = true,
                    Status = EquipmentStatus.Available
                },
                new Equipment
                {
                    Id = 10,
                    Name = "Chainsaw",
                    Category = EquipmentCategory.PowerTools,
                    Condition = EquipmentCondition.Good,
                    Description = "Stihl Chainsaw",
                    IsAvailable = true,
                    Status = EquipmentStatus.Available
                }
            );



            modelBuilder.Entity<Customer>()
                .Property(c => c.Role)
                .HasConversion<string>();


            // Seed data for Customer
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Admin",
                    Username = "admin",
                    Password = "admin",
                    Role = Customer.UserRole.Admin,
                    IsActive = true
                },
                new Customer
                {
                    Id = 2,
                    Name = "John Doe",
                    Username = "john",
                    Password = "john123",
                    Role = Customer.UserRole.Admin,
                    IsActive = true
                },
                new Customer
                {
                    Id = 3,
                    Name = "Jane Smith",
                    Username = "jane",
                    Password = "jane123",
                    Role = Customer.UserRole.User,
                    IsActive = true
                },
                new Customer
                {
                    Id = 4,
                    Name = "Alex Johnson",
                    Username = "alex",
                    Password = "alex",
                    Role = Customer.UserRole.User,
                    IsActive = true
                },
                new Customer
                {
                    Id = 5,
                    Name = "Maria Garcia",
                    Username = "maria",
                    Password = "maria",
                    Role = Customer.UserRole.User,
                    IsActive = true
                },
                new Customer
                {
                    Id = 6,
                    Name = "User1",
                    Username = "user1",
                    Password = "user1",
                    Role = Customer.UserRole.User,
                    IsActive = true
                },
                new Customer
                {
                    Id = 7,
                    Name = "User2",
                    Username = "user2",
                    Password = "user2",
                    Role = Customer.UserRole.User,
                    IsActive = true
                },
                new Customer
                {
                    Id = 8,
                    Name = "User3",
                    Username = "user3",
                    Password = "user3",
                    Role = Customer.UserRole.User,
                    IsActive = true
                },
                new Customer
                {
                    Id = 9,
                    Name = "User4",
                    Username = "User4",
                    Password = "User4",
                    Role = Customer.UserRole.User,
                    IsActive = true
                },
                new Customer
                {
                    Id = 10,
                    Name = "User5",
                    Username = "user5",
                    Password = "user5",
                    Role = Customer.UserRole.User,
                    IsActive = true
                }
            );


            // Relationships
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(r => r.Rentals)
                .HasForeignKey(r => r.CustomerId);


            //Seed data for Rental
            modelBuilder.Entity<Rental>().HasData(
               new Rental
               {
                   Id = 1,
                   CustomerId = 1,
                   EquipmentId = 7,
                   IssuedAt = DateTime.UtcNow.AddDays(-10),
                   DueDate = DateTime.UtcNow.AddDays(10),
                   ReturnedAt = null,
                   Status = Rental.RentalStatus.Active,
                   ReturnNotes = "",
                   EquipmentCondition = Equipment.EquipmentCondition.New,
                   EquipmentStatus = Equipment.EquipmentStatus.Rented
               },
               new Rental
               {
                   Id = 2,
                   CustomerId = 2,
                   EquipmentId = 7,
                   IssuedAt = DateTime.UtcNow.AddDays(-5),
                   DueDate = DateTime.UtcNow.AddDays(5),
                   ReturnedAt = null,
                   Status = Rental.RentalStatus.Active,
                   ReturnNotes = "",
                   EquipmentCondition = Equipment.EquipmentCondition.New,
                   EquipmentStatus = Equipment.EquipmentStatus.Rented
               },
               new Rental
               {
                   Id = 3,
                   CustomerId = 3,
                   EquipmentId = 5,
                   IssuedAt = DateTime.UtcNow.AddDays(-20),
                   DueDate = DateTime.UtcNow.AddDays(-10),
                   ReturnedAt = DateTime.UtcNow.AddDays(-9),
                   Status = Rental.RentalStatus.Returned,
                   ReturnNotes = "Returned in good condition",
                   EquipmentCondition = Equipment.EquipmentCondition.Good,
                   EquipmentStatus = Equipment.EquipmentStatus.Available

               },
               new Rental
               {
                   Id = 4,
                   CustomerId = 4,
                   EquipmentId = 5,
                   IssuedAt = DateTime.UtcNow.AddDays(-2),
                   DueDate = DateTime.UtcNow.AddDays(12),
                   ReturnedAt = null,
                   Status = Rental.RentalStatus.Active,
                   ReturnNotes = "",
                   EquipmentCondition = Equipment.EquipmentCondition.Good,
                   EquipmentStatus = Equipment.EquipmentStatus.Rented
               },
               new Rental
               {
                   Id = 5,
                   CustomerId = 5,
                   EquipmentId = 2,
                   IssuedAt = DateTime.UtcNow.AddDays(-30),
                   DueDate = DateTime.UtcNow.AddDays(-5),
                   ReturnedAt = null,
                   Status = Rental.RentalStatus.Overdue,
                   ReturnNotes = "",
                   EquipmentCondition = Equipment.EquipmentCondition.Excellent,
                   EquipmentStatus = Equipment.EquipmentStatus.Rented
               },
               new Rental
               {
                   Id = 6,
                   CustomerId = 6,
                   EquipmentId = 3,
                   IssuedAt = DateTime.UtcNow.AddDays(-1),
                   DueDate = DateTime.UtcNow.AddDays(6),
                   ReturnedAt = null,
                   Status = Rental.RentalStatus.Active,
                   ReturnNotes = "",
                   EquipmentCondition = Equipment.EquipmentCondition.Excellent,
                   EquipmentStatus = Equipment.EquipmentStatus.Rented
               },
               new Rental
               {
                   Id = 7,
                   CustomerId = 7,
                   EquipmentId = 4,
                   IssuedAt = DateTime.UtcNow.AddDays(-15),
                   DueDate = DateTime.UtcNow.AddDays(-1),
                   ReturnedAt = DateTime.UtcNow.AddDays(-1),
                   Status = Rental.RentalStatus.Returned,
                   ReturnNotes = "Chain replaced",
                   EquipmentCondition = Equipment.EquipmentCondition.Fair,
                   EquipmentStatus = Equipment.EquipmentStatus.Available
               }
           );

        }
    }

}

