using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Cancel
{
    public record CancelShipmentUnitCommand(Guid shipmentUnitId) : IRequest<bool>;

    public class CancelShipmentUnitCommandHandler : IRequestHandler<CancelShipmentUnitCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;

        public CancelShipmentUnitCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(CancelShipmentUnitCommand command, CancellationToken cancellationToken)
        {
            var unit = await _dbContext.ShipmentUnits.FindAsync(new object[] { command.shipmentUnitId }, cancellationToken);
            if (unit == null) return false;

            unit.Cancel();
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
