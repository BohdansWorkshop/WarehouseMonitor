using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Application.Warehouses.Commands.Create;

public record CreateWarehouseCommand(string name, string branchCode, string address) : IRequest<Guid>;
public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateWarehouseCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken)
    {
        var entity = new Warehouse(command.name, command.branchCode, command.address);
        _dbContext.Warehouses.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
