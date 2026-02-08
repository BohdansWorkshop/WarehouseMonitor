using System;
using System.Collections.Generic;
using System.Text;
using WarehouseMonitor.Domain.Common;

namespace WarehouseMonitor.Domain.Entities;

public class Warehouse : BaseEntity
{
    public string Name { get; set; }
    public string BranchCode { get; set; }
    public string Address { get; set; }

    private Warehouse() { } // EF Core support

    public Warehouse(string name, string branchCode, string address)
    {
        Name = name;
        BranchCode = branchCode;
        Address = address;
    }
}

