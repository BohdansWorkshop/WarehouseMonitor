namespace WarehouseMonitor.Application.ShipmentUnits;

public class ShipmentUnitDto
{
    public Guid Id { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;       
    public decimal Weight { get; set; }
    public decimal Volume { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;  
    public Guid? CurrentWarehouseId { get; set; }
    public string? CurrentWarehouse { get; set; }           

    public Guid? TargetWarehouseId { get; set; }
    public string? TargetWarehouse { get; set; }            
}