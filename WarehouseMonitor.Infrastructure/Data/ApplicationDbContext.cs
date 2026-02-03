using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Domain.Entities;
using WarehouseMonitor.Infrastructure.Identity;

namespace WarehouseMonitor.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


        public DbSet<Product> Products => Set<Product>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<ShipmentUnit> ShipmentUnits => Set<ShipmentUnit>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base action to setup identity tables first
            // Setup Microsoft Identity tables (AspNetUsers, AspNetRoles, etc.)
            base.OnModelCreating(modelBuilder);

            // Apply all IEntityTypeConfiguration<T> implementations in current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
