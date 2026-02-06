using MediatR;
using Microsoft.EntityFrameworkCore;
using WarehouseMonitor.Application.Common.Interfaces;

namespace WarehouseMonitor.Application.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid id) : IRequest<bool>;
    public class DeleteProductCommandHandler: IRequestHandler<DeleteProductCommand, bool> 
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteProductCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var entityToRemove = await _dbContext.Products
                .Where(x=>x.Id == command.id).
                SingleOrDefaultAsync(cancellationToken);
            
            if(entityToRemove == null)
            {
                return false;
            }

            _dbContext.Products.Remove(entityToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
