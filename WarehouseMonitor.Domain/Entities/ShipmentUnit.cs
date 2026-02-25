using WarehouseMonitor.Domain.Common;
using WarehouseMonitor.Domain.Enums;
using WarehouseMonitor.Domain.Events;

namespace WarehouseMonitor.Domain.Entities;

public class ShipmentUnit : BaseEntity
{
    public string TrackingNumber { get; set; } = string.Empty;
    public ShipmentStatus Status { get; set; }
    public decimal Weight { get; set; }
    public decimal Volume { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid? CurrentWarehouseId { get; set; }
    public Warehouse? CurrentWarehouse { get; set; }

    public Guid? TargetWarehouseId { get; set; }

    private ShipmentUnit() { } // EF Core

    public ShipmentUnit(string trackingNumber, decimal weight, decimal volume, Guid productId)
    {
        if (string.IsNullOrWhiteSpace(trackingNumber))
            throw new ArgumentException("Tracking number required");

        if (weight <= 0 || volume <= 0)
            throw new ArgumentException("Weight and volume must be positive");

        TrackingNumber = trackingNumber;
        Weight = weight;
        Volume = volume;
        ProductId = productId;
        Status = ShipmentStatus.Created;
    }

    public void ReceiveAtWarehouse(Guid warehouseId)
    {
        if (Status == ShipmentStatus.Delivered || Status == ShipmentStatus.Cancelled)
            throw new InvalidOperationException("Cannot receive delivered or cancelled shipment");

        if (CurrentWarehouseId == warehouseId && Status == ShipmentStatus.Received)
            throw new InvalidOperationException("Already received at this warehouse");

        CurrentWarehouseId = warehouseId;
        Status = ShipmentStatus.Received;
        RaiseEvent(new ShipmentUnitReceivedEvent(Id, warehouseId));
    }

    public void Ship(Guid targetWarehouseId)
    {
        if (Status != ShipmentStatus.Received)
            throw new InvalidOperationException("Only received shipments can be shipped");

        if (targetWarehouseId == CurrentWarehouseId)
            throw new InvalidOperationException("Target warehouse must be different from current warehouse");

        TargetWarehouseId = targetWarehouseId;
        Status = ShipmentStatus.InTransit;

        RaiseEvent(new ShipmentUnitShipEvent(Id, targetWarehouseId));
    }

    public void Deliver()
    {
        if (Status != ShipmentStatus.InTransit)
            throw new InvalidOperationException("Only in-transit shipments can be delivered");

        Status = ShipmentStatus.Delivered;

        RaiseEvent(new ShipmentUnitDeliveredEvent(Id));
    }

    public void Cancel()
    {
        if (Status == ShipmentStatus.Delivered)
            throw new InvalidOperationException("Cannot cancel a delivered shipment");

        Status = ShipmentStatus.Cancelled;
        TargetWarehouseId = null;

        RaiseEvent(new ShipmentUnitCancelledEvent(Id));
    }
}
