using WarehouseMonitor.Domain.Common;

namespace WarehouseMonitor.Domain.Events;

public class ShipmentUnitCancelledEvent : DomainEvent
{
    public Guid ShipmentUnitId { get; }

    public ShipmentUnitCancelledEvent(Guid shipmentUnitId)
    {
        ShipmentUnitId = shipmentUnitId;
    }
}
