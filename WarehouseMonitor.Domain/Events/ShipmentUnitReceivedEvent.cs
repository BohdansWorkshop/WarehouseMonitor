using WarehouseMonitor.Domain.Common;

namespace WarehouseMonitor.Domain.Events;

public sealed class ShipmentUnitReceivedEvent : DomainEvent
{
    public Guid ShipmentUnitId { get; }
    public Guid WarehouseId { get; }

    public ShipmentUnitReceivedEvent(Guid shipmentUnitId, Guid warehouseId)
    {
        ShipmentUnitId = shipmentUnitId;
        WarehouseId = warehouseId;
    }
}