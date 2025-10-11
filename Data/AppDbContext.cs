using Midterm_EquipmentRental_Team2.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Midterm_EquipmentRental_Team2.Data
{
    public class AppDbContext: DbContext
    {
        //public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
     

        // seed data into database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Username = "admin", Password = "admin", Role = "Admin", IsActive = true }, // admin
                    new User { Id = 2, Username = "user1", Password = "user1", Role = "user", IsActive = true },
                    new User { Id = 3, Username = "user2", Password = "user2", Role = "user", IsActive = true },
                    new User { Id = 4, Username = "user3", Password = "user3", Role = "user", IsActive = true }
             );
            //// Relationships
            //modelBuilder.Entity<Rental>()
            //    .HasOne(r => r.Customer)
            //    .WithMany(c => c.Rentals)
            //    .HasForeignKey(r => r.CustomerId);

            //modelBuilder.Entity<Rental>()
            //    .HasOne(r => r.Equipment)
            //    .WithMany(e => e.Rentals)
            //    .HasForeignKey(r => r.EquipmentId);

            //// Seed data
            //modelBuilder.Entity<Customer>().HasData(
            //    new Customer
            //    {
            //        Id = 1,
            //        Name = "Admin User",
            //        Username = "admin",
            //        Password = "admin123", // For demo only; use hashing in production
            //        Role = "Admin",
            //        IsActive = true
            //    },
            //    new Customer
            //    {
            //        Id = 2,
            //        Name = "John Doe",
            //        Username = "john",
            //        Password = "john123",
            //        Role = "User",
            //        IsActive = true
            //    },
            //    new Customer
            //    {
            //        Id = 3,
            //        Name = "Jane Smith",
            //        Username = "jane",
            //        Password = "jane123",
            //        Role = "User",
            //        IsActive = true
            //    }
            //);

            //modelBuilder.Entity<Equipment>().HasData(
            //    new Equipment
            //    {
            //        Id = 1,
            //        Name = "Excavator",
            //        Category = "Heavy Machinery",
            //        Condition = "Excellent",
            //        Description = "CAT 320 Excavator",
            //        IsAvailable = true,
            //        Status = "Available"
            //    },
            //    new Equipment
            //    {
            //        Id = 2,
            //        Name = "Jackhammer",
            //        Category = "Power Tools",
            //        Condition = "Good",
            //        Description = "Bosch Electric Jackhammer",
            //        IsAvailable = true,
            //        Status = "Available"
            //    },
            //    new Equipment
            //    {
            //        Id = 3,
            //        Name = "Surveying Drone",
            //        Category = "Surveying",
            //        Condition = "New",
            //        Description = "DJI Phantom 4 RTK",
            //        IsAvailable = true,
            //        Status = "Available"
            //    }
            //);

            // Rentals can be seeded if needed, but typically start empty
        }

    }
}
