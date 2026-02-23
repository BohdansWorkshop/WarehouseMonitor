using MediatR;
using Microsoft.Extensions.Logging;
using WarehouseMonitor.Application.Common;
using WarehouseMonitor.Domain.Events;

namespace WarehouseMonitor.Application.ShipmentUnits.EventHandlers
{
    public sealed class ShipmentUnitReceivedEventHandler : INotificationHandler<DomainEventNotification>
    {
        private readonly ILogger<ShipmentUnitReceivedEventHandler> _logger;

        public ShipmentUnitReceivedEventHandler(
            ILogger<ShipmentUnitReceivedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification notification, CancellationToken cancellationToken)
        {
            if (notification.Event is not ShipmentUnitReceivedEvent e)
                return Task.CompletedTask;

            _logger.LogInformation($"ShipmentUnit RECEIVED | ShipmentUnitId: {e.ShipmentUnitId} | WarehouseId: {e.WarehouseId} | Time: {e.OccurredOn}");
            return Task.CompletedTask;
        }
    }
}
