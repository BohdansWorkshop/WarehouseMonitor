using WarehouseMonitor.Domain.Common;
using WarehouseMonitor.Domain.Enums;

namespace WarehouseMonitor.Domain.Entities;

public class ShipmentUnit : BaseEntity
{ 
   public string TrackingNumber { get; set; }
    public ShipmentStatus Status { get; set; }

    public decimal Weight { get; set; }
    public decimal Volume { get; set; }

    public Guid ProductId { get; set; } 

    public Guid? CurrentWarehouseId { get; set; }
    public Warehouse? CurrentWarehouse { get; set; }

    private ShipmentUnit() { } // EF Core support

    public ShipmentUnit(string trackingNumber, decimal weight, decimal volume, Guid productId)
    {
        TrackingNumber = trackingNumber;
        Weight = weight;
        Volume = volume;
        ProductId = productId;
        Status = ShipmentStatus.Created;
    }

    public void ReceiveAtWarehouse(Guid warehouseId)
    {
        if (Status == ShipmentStatus.Delivered)
            throw new InvalidOperationException("Unable to receive, unit is already delivered");

        CurrentWarehouseId = warehouseId;
        Status = ShipmentStatus.Received;
    }

}