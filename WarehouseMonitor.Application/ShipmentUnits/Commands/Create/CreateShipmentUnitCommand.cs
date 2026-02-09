using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Create;

public record CreateShipmentUnitCommand(
    string trackingNumber,
    decimal weight,
    decimal volume,
    Guid productId
) : IRequest<Guid>;

public class CreateShipmentUnitCommandHandler
    : IRequestHandler<CreateShipmentUnitCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateShipmentUnitCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateShipmentUnitCommand command, CancellationToken cancellationToken)
    {
        var entity = new ShipmentUnit(
            command.trackingNumber,
            command.weight,
            command.volume,
            command.productId
        );

        _dbContext.ShipmentUnits.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
