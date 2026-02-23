using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Ship
{
    public record ShipShipmentUnitCommand(Guid shipmentUnitId, Guid warehouseId) : IRequest<bool>;

    public class ShipShipmentUnitCommandHandler : IRequestHandler<ShipShipmentUnitCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;

        public ShipShipmentUnitCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(ShipShipmentUnitCommand command, CancellationToken cancellationToken)
        {
            var shipmentUnit = await _dbContext.ShipmentUnits.FindAsync(new[] { command.shipmentUnitId }, cancellationToken);
            if (shipmentUnit == null){
                return false;
            }

            shipmentUnit.ReceiveAtWarehouse(command.warehouseId);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;


        }
    }
}
