using MediatR;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Application.Products.Commands.Update;

public record UpdateProductCommand(Product product) : IRequest<bool>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateProductCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var updatedEntity = await _dbContext.Products.FindAsync(new[] { command.product.Id });
        if(updatedEntity == null)
        {
            return false;
        }

        updatedEntity.Description = command.product.Description;
        updatedEntity.Name = command.product.Name;
        updatedEntity.Type = command.product.Type;
        updatedEntity.SKU = command.product.SKU;
        updatedEntity.LastModified= DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

}
