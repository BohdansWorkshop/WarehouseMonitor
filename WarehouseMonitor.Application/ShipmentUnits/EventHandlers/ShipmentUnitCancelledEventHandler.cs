using MediatR;
using Microsoft.Extensions.Logging;
using WarehouseMonitor.Application.Common;
using WarehouseMonitor.Domain.Events;

namespace WarehouseMonitor.Application.ShipmentUnits.EventHandlers;

public class ShipmentUnitCancelledEventHandler : INotificationHandler<DomainEventNotification>
{
    private readonly ILogger<ShipmentUnitCancelledEventHandler> _logger;

    public ShipmentUnitCancelledEventHandler(
        ILogger<ShipmentUnitCancelledEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Event is not ShipmentUnitCancelledEvent e)
        {
            return Task.CompletedTask;
        }

        _logger.LogInformation($"ShipmentUnit DELIVERED | ShipmentUnitId: {e.ShipmentUnitId} | Time: {e.OccurredOn}");
        return Task.CompletedTask;
    }
}

