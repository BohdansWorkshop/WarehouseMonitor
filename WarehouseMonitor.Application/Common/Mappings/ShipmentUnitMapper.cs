using WarehouseMonitor.Application.ShipmentUnits;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Application.Common.Mappings;

public static class ShipmentUnitMapper
{
    public static ShipmentUnitDto MapToDto(this ShipmentUnit entity)
    {
        return new ShipmentUnitDto
        {
            Id = entity.Id,
            TrackingNumber = entity.TrackingNumber,
            Status = entity.Status.ToString(),
            Weight = entity.Weight,
            Volume = entity.Volume,
            ProductId = entity.ProductId,
            CurrentWarehouseId = entity.CurrentWarehouseId,
            TargetWarehouseId = entity.TargetWarehouseId
        };
    }
}