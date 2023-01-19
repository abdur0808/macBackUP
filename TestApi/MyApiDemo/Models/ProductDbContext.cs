using System;
using Microsoft.EntityFrameworkCore;
using MyApiDemo.Models;


namespace MyApiDemo.Model
{

    public partial class ProductDbContext: DbContext
    {
        public ProductDbContext()
        { }
        public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasColumnType("VARCHAR(500)");
                entity.Property(e => e.LastName).HasColumnType("VARCHAR(500)");
                entity.Property(e => e.Title).HasColumnType("VARCHAR(500)");
                entity.Property(e => e.Address).HasColumnType("VARCHAR(500)");
                entity.Property(e => e.City).HasColumnType("VARCHAR(500)");
                entity.Property(e => e.State).HasColumnType("VARCHAR(500)");
                entity.Property(e => e.Country).HasColumnType("VARCHAR(500)");
                entity.Property(e => e.Phone).HasColumnType("VARCHAR(500)");
                entity.Property(e => e.Fax).HasColumnType("VARCHAR(500)");
                entity.Property(e => e.Email).HasColumnType("VARCHAR(500)");
                });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
