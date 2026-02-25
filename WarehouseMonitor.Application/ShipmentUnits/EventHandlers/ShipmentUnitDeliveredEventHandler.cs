using MediatR;
using Microsoft.Extensions.Logging;
using WarehouseMonitor.Application.Common;
using WarehouseMonitor.Domain.Events;

namespace WarehouseMonitor.Application.ShipmentUnits.EventHandlers
{
    public class ShipmentUnitDeliveredEventHandler : INotificationHandler<DomainEventNotification>
    {
        private readonly ILogger<ShipmentUnitDeliveredEventHandler> _logger;

        public ShipmentUnitDeliveredEventHandler(ILogger<ShipmentUnitDeliveredEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification notification, CancellationToken cancellationToken)
        {
            if (notification.Event is not ShipmentUnitDeliveredEvent e)
            {
                return Task.CompletedTask;
            }

            _logger.LogInformation($"ShipmentUnit DELIVERED | ShipmentUnitId: {e.ShipmentUnitId} | Time: {e.OccurredOn}");
            return Task.CompletedTask;
        }
    }
}
