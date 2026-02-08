using FluentValidation;

namespace WarehouseMonitor.Application.Products.Commands.Delete;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("Id must not be empty!");
    }
}
