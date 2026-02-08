using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.Warehouses.Commands.Delete;

public record DeleteWarehouseCommand(Guid id) : IRequest<bool>;
public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, bool>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteWarehouseCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteWarehouseCommand command, CancellationToken cancellationToken)
    {
        var entityToRemove = await _dbContext.Warehouses.FindAsync(new[] { command.id });
        if (entityToRemove == null)
        {
            return false;
        }

        _dbContext.Warehouses.Remove(entityToRemove);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}

