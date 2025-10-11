using Midterm_EquipmentRental_Team2.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Midterm_EquipmentRental_Team2.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationships
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Equipment)
                .WithMany(e => e.Rentals)
                .HasForeignKey(r => r.EquipmentId);

            // Seed data
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

            modelBuilder.Entity<Equipment>().HasData(
                new Equipment
                {
                    Id = 1,
                    Name = "Excavator",
                    Category = "Heavy Machinery",
                    Condition = "Excellent",
                    Description = "CAT 320 Excavator",
                    IsAvailable = true,
                    Status = "Available"
                },
                new Equipment
                {
                    Id = 2,
                    Name = "Jackhammer",
                    Category = "Power Tools",
                    Condition = "Good",
                    Description = "Bosch Electric Jackhammer",
                    IsAvailable = true,
                    Status = "Available"
                },
                new Equipment
                {
                    Id = 3,
                    Name = "Surveying Drone",
                    Category = "Surveying",
                    Condition = "New",
                    Description = "DJI Phantom 4 RTK",
                    IsAvailable = true,
                    Status = "Available"
                }
            );

            // Rentals can be seeded if needed, but typically start empty
        }

    }
}
