
using WarehouseMonitor.Domain.Common;

namespace WarehouseMonitor.Domain.Events;

public sealed class ShipmentUnitDeliveredEvent : DomainEvent
{
    public Guid ShipmentUnitId { get; }

    public ShipmentUnitDeliveredEvent(Guid shipmentUnitId)
    {
        ShipmentUnitId = shipmentUnitId;
    }
}