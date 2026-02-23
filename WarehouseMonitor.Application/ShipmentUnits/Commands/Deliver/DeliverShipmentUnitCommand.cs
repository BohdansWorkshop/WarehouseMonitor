using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Deliver
{
    public record DeliverShipmentUnitCommand(Guid shipmentUnitId) : IRequest<bool>;

    public class DeliverShipmentUnitCommandHandler : IRequestHandler<DeliverShipmentUnitCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeliverShipmentUnitCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeliverShipmentUnitCommand command, CancellationToken cancellationToken)
        {
            var unit = await _dbContext.ShipmentUnits.FindAsync(new object[] { command.shipmentUnitId }, cancellationToken);
            if (unit == null) return false;

            unit.Deliver();
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
