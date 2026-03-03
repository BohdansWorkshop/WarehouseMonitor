using MediatR;
using Microsoft.EntityFrameworkCore;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.Warehouses.Queries;

public record GetWarehousesQuery : IRequest<IEnumerable<WarehouseDto>>;

public class GetWarehousesQueryHandler : IRequestHandler<GetWarehousesQuery, IEnumerable<WarehouseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetWarehousesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WarehouseDto>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Warehouses
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return entities.Select(w => new WarehouseDto
        {
            Id = w.Id,
            Name = w.Name,
            Address = w.Address,
            BranchCode = w.BranchCode
        });
    }
}