using Microsoft.EntityFrameworkCore;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<Product> Products { get; }

    DbSet<Warehouse> Warehouses { get; }

    DbSet<ShipmentUnit> ShipmentUnits { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
