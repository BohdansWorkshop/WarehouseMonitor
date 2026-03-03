using MediatR;
using Microsoft.EntityFrameworkCore;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.Products.Queries;

public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return entities.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            SKU = p.SKU,
            Type = p.Type.ToString()
        });
    }
}