using System;
using System.Collections.Generic;
using System.Text;
using WarehouseMonitor.Domain.Enums;

namespace WarehouseMonitor.Application.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
}
