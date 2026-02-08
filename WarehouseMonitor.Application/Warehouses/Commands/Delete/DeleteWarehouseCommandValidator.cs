using FluentValidation;

namespace WarehouseMonitor.Application.Warehouses.Commands.Delete;

public class DeleteWarehouseCommandValidator : AbstractValidator<DeleteWarehouseCommand>
{
    public DeleteWarehouseCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("Id must not be empty!");
    }
}
