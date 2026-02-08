using System;
using System.Collections.Generic;
using System.Text;
using WarehouseMonitor.Domain.Common;
using WarehouseMonitor.Domain.Enums;

namespace WarehouseMonitor.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public ProductType Type { get; set; } = ProductType.Standard;

    private Product() { } // EF core support

    public Product(string name, string sku, ProductType type = ProductType.Standard)
    {
        Name = name;
        SKU = sku;
        Type = type;
    }
}
