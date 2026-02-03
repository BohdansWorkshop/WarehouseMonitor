namespace WarehouseMonitor.Domain.Enums;

public enum ShipmentStatus
{
    Created = 1,
    Received = 2,
    InTransit = 3,
    Arrived = 4,
    Delivered = 5,
    Lost = 6,
    Damaged = 7
}