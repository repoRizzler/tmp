
using koll_2.Models;
using Microsoft.EntityFrameworkCore;

namespace ForestNursery.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Nursery> Nurseries { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Responsible> Responsibles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Batch>()
                .HasOne(b => b.Nursery)
                .WithMany(n => n.Batches)
                .HasForeignKey(b => b.NurseryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Batch>()
                .HasOne(b => b.Species)
                .WithMany(s => s.Batches)
                .HasForeignKey(b => b.SpeciesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Responsible>()
                .HasOne(be => be.Batch)
                .WithMany(b => b.BatchEmployees)
                .HasForeignKey(be => be.BatchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Responsible>()
                .HasOne(be => be.Employee)
                .WithMany(e => e.BatchEmployees)
                .HasForeignKey(be => be.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Nursery>().HasData(
                new Nursery { NurseryId = 1, Name = "Green Forest Nursery", EstablishedDate = new DateTime(2005, 5, 10) },
                new Nursery { NurseryId = 2, Name = "Pine Valley Nursery", EstablishedDate = new DateTime(2010, 3, 15) }
            );

            modelBuilder.Entity<Species>().HasData(
                new Species { SpeciesId = 1, LatinName = "Quercus robur", GrowthTimeInYears = 5 },
                new Species { SpeciesId = 2, LatinName = "Pinus sylvestris", GrowthTimeInYears = 3 },
                new Species { SpeciesId = 3, LatinName = "Fagus sylvatica", GrowthTimeInYears = 7 }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FirstName = "Anna", LastName = "Kowalska" },
                new Employee { EmployeeId = 2, FirstName = "Jan", LastName = "Nowak" },
                new Employee { EmployeeId = 3, FirstName = "Maria", LastName = "Wiśniewska" }
            );

            modelBuilder.Entity<Batch>().HasData(
                new Batch 
                { 
                    BatchId = 1, 
                    Quantity = 500, 
                    SownDate = new DateTime(2024, 3, 15), 
                    ReadyDate = new DateTime(2029, 3, 15), 
                    NurseryId = 1, 
                    SpeciesId = 1 
                }
            );

            modelBuilder.Entity<Responsible>().HasData(
                new Responsible { BatchId = 1, EmployeeId = 1, Role = "Supervisor" },
                new Responsible { BatchId = 1, EmployeeId = 2, Role = "Planter" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
