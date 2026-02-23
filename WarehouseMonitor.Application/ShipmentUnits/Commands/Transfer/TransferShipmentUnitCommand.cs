using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Transfer
{
    public record class TransferShipmentUnitCommand(Guid shipmentUnitId, Guid targetWarehouseId) : IRequest<bool>;

    public class TransferShipmentUnitCommandHandler : IRequestHandler<TransferShipmentUnitCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;
        public TransferShipmentUnitCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(TransferShipmentUnitCommand command, CancellationToken cancellationToken)
        {
            var shipmentUnit = await _dbContext.ShipmentUnits.FindAsync(new[] { command.shipmentUnitId });
            if (shipmentUnit == null)
            {
                return false;
            }

            shipmentUnit.Ship(command.targetWarehouseId);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
