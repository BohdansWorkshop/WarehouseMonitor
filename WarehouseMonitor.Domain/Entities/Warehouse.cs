using System;
using System.Collections.Generic;
using System.Text;
using WarehouseMonitor.Domain.Common;

namespace WarehouseMonitor.Domain.Entities;

public class Warehouse : BaseEntity
{
    public string Name { get; private set; }
    public string BranchCode { get; private set; }
    public string Address { get; private set; }

    private Warehouse() { } // EF Core support

    public Warehouse(string name, string branchCode, string address)
    {
        Name = name;
        BranchCode = branchCode;
        Address = address;
    }
}

