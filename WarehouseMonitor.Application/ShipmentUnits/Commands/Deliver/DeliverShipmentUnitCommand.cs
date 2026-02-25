using MediatR;
using WarehouseMonitor.Application.Common;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Deliver
{
    public record DeliverShipmentUnitCommand(Guid shipmentUnitId) : IRequest<bool>;

    public class DeliverShipmentUnitCommandHandler : IRequestHandler<DeliverShipmentUnitCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public DeliverShipmentUnitCommandHandler(IApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeliverShipmentUnitCommand command, CancellationToken cancellationToken)
        {
            var shipmentUnit = await _dbContext.ShipmentUnits.FindAsync(new object[] { command.shipmentUnitId }, cancellationToken);
            if (shipmentUnit == null) return false;

            shipmentUnit.Deliver();
            await _dbContext.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in shipmentUnit.PullDomainEvents())
            {
                await _mediator.Publish(new DomainEventNotification(domainEvent), cancellationToken);
            }

            return true;
        }
    }
}
