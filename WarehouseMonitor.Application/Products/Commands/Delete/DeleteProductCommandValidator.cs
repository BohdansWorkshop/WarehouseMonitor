using FluentValidation;

namespace WarehouseMonitor.Application.Products.Commands;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("Guid must not be empty!");
    }
}
