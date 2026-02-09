using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Delete
{
    public record DeleteShipmentUnitCommand(Guid id) : IRequest<bool>;
    public class DeleteShipmentUnitCommandHandler : IRequestHandler<DeleteShipmentUnitCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteShipmentUnitCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteShipmentUnitCommand command, CancellationToken cancellationToken)
        {
            var entityToRemove = await _dbContext.ShipmentUnits.FindAsync(new[] { command.id }, cancellationToken);
            if(entityToRemove == null)
            {
                return false;
            }
            _dbContext.ShipmentUnits.Remove(entityToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
