// Domain/Common/BaseEntity.cs
namespace WarehouseMonitor.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTimeOffset Created { get; set; } 
    public DateTimeOffset LastModified { get; set; }
}