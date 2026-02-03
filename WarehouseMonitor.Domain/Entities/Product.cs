using System;
using System.Collections.Generic;
using System.Text;
using WarehouseMonitor.Domain.Common;
using WarehouseMonitor.Domain.Enums;

namespace WarehouseMonitor.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string SKU { get; private set; } = string.Empty;
    
    public string Description { get; private set; } = string.Empty; 

    public ProductType Type { get; private set; }

    private Product() { } // EF core support

    public Product(string name, string sku, ProductType type)
    {
        Name = name;
        SKU = sku;
        Type = type;
    }
}
