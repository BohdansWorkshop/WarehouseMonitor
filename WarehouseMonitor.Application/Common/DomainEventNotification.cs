using MediatR;
using WarehouseMonitor.Domain.Common;

namespace WarehouseMonitor.Application.Common;

public sealed class DomainEventNotification : INotification
{
    public DomainEvent Event { get; }

    public DomainEventNotification(DomainEvent domainEvent)
    {
        Event = domainEvent;
    }
}
