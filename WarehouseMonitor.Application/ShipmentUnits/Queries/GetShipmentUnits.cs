using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WarehouseMonitor.Application.ShipmentUnits.Queries;

public record GetShipmentUnitsQuery : IRequest<IEnumerable<ShipmentUnitDto>>;

public class GetShipmentUnitsRequestHandler : IRequestHandler<GetShipmentUnitsQuery, IEnumerable<ShipmentUnitDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetShipmentUnitsRequestHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ShipmentUnitDto>> Handle(GetShipmentUnitsQuery query, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.ShipmentUnits
            .Include(s => s.Product)
            .Include(s => s.CurrentWarehouse)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return entities.Select(MapToDto);
    }

    private ShipmentUnitDto MapToDto(ShipmentUnit entity)
    {
        return new ShipmentUnitDto
        {
            Id = entity.Id,
            TrackingNumber = entity.TrackingNumber,
            Status = entity.Status.ToString(),
            Weight = entity.Weight,
            Volume = entity.Volume,
            ProductId = entity.ProductId,
            ProductName = entity.Product?.Name ?? string.Empty,
            CurrentWarehouseId = entity.CurrentWarehouseId,
            CurrentWarehouse = entity.CurrentWarehouse?.Name,
            TargetWarehouseId = entity.TargetWarehouseId,
            TargetWarehouse = null
        };
    }
}