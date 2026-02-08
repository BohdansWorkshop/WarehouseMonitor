using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Application.Warehouses.Commands.Update;

public record UpdateWarehouseCommand(Warehouse warehouse) : IRequest<bool>;
public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, bool>
{
    private readonly IApplicationDbContext _dbContext;
    public UpdateWarehouseCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken)
    {
        var existentEntity = await _dbContext.Warehouses.FindAsync(new []{ command.warehouse.Id });
        if(existentEntity == null)
        {
            return false;
        }

        existentEntity.Name = command.warehouse.Name;
        existentEntity.Address = command.warehouse.Address;
        existentEntity.BranchCode = command.warehouse.BranchCode;
        existentEntity.BranchCode = command.warehouse.BranchCode;
        existentEntity.LastModified = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}


