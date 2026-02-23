using MediatR;
using WarehouseMonitor.Application.Common;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Receive
{
    public record ReceiveShipmentUnitCommand(Guid shipmentUnitId, Guid warehouseId) : IRequest<bool>;

    public class ReceiveShipmentUnitCommandHandler : IRequestHandler<ReceiveShipmentUnitCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public ReceiveShipmentUnitCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<bool> Handle(ReceiveShipmentUnitCommand command, CancellationToken cancellationToken)
        {
            var shipmentUnit = await _dbContext.ShipmentUnits.FindAsync(new[] { command.shipmentUnitId}, cancellationToken);
            if (shipmentUnit == null)
            {
                return false;
            }

            shipmentUnit.ReceiveAtWarehouse(command.warehouseId);
            await _dbContext.SaveChangesAsync(cancellationToken);

            foreach(var domainEvent in shipmentUnit.PullDomainEvents())
            {
                await _mediator.Publish(new DomainEventNotification(domainEvent), cancellationToken);
            }

            return true;
        }
    }
}
