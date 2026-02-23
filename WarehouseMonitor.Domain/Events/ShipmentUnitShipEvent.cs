using WarehouseMonitor.Domain.Common;

namespace WarehouseMonitor.Domain.Events
{
    public class ShipmentUnitShipEvent : DomainEvent
    {
        public Guid ShipmentUnitId { get; }
        public Guid TargetWarehouseId { get; }

        public ShipmentUnitShipEvent(Guid shipmentUnitId, Guid targetWarehouseId)
        {
            ShipmentUnitId = shipmentUnitId;
            TargetWarehouseId = targetWarehouseId;
        }
    }
}
