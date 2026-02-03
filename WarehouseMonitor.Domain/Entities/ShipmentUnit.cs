using System;
using System.Collections.Generic;
using System.Text;
using WarehouseMonitor.Domain.Common;
using WarehouseMonitor.Domain.Enums;

namespace WarehouseMonitor.Domain.Entities;

public class ShipmentUnit : BaseEntity
{
    public string TrackingNumber { get; private set; } = string.Empty;
    public ShipmentStatus Status { get; private set; }

    public decimal Weight { get; private set; }
    public decimal Volume { get; private set; }

    public Guid ProductId { get; private set; } 
    public Product Product { get; private set; } = null!;   

    public Guid? CurrentWarehouseId { get; private set; }
    public Warehouse? CurrentWarehouse { get; private set; }

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