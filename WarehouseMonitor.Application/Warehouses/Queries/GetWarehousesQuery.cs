using MediatR;
using Microsoft.EntityFrameworkCore;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Application.Warehouses.Queries;

public record GetWarehousesQuery : IRequest<IEnumerable<Warehouse>>;


public class GetWarehousesQueryHandler : IRequestHandler<GetWarehousesQuery, IEnumerable<Warehouse>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetWarehousesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Warehouse>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Warehouses.ToListAsync(cancellationToken);
    }
}
