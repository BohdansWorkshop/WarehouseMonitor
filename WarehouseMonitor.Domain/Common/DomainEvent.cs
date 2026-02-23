using System;

namespace WarehouseMonitor.Domain.Common
{
    public abstract class DomainEvent 
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
