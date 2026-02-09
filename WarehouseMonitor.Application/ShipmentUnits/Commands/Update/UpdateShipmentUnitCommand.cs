using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Update;

public record UpdateShipmentUnitCommand(
    Guid Id,
    string TrackingNumber,
    decimal Weight,
    decimal Volume,
    Guid ProductId
) : IRequest<bool>;

public class UpdateShipmentUnitCommandHandler
    : IRequestHandler<UpdateShipmentUnitCommand, bool>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateShipmentUnitCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateShipmentUnitCommand command, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.ShipmentUnits
            .FindAsync(new object[] { command.Id }, cancellationToken);

        if (entity == null)
            return false;

        entity.TrackingNumber = command.TrackingNumber;
        entity.Weight = command.Weight;
        entity.Volume = command.Volume;
        entity.ProductId = command.ProductId;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
