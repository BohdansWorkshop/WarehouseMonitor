using FluentValidation;
using WarehouseMonitor.Domain.Constants;

namespace WarehouseMonitor.Application.Warehouses.Commands.Create;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {

        RuleFor(v => v.branchCode).NotEmpty().WithMessage("Branch code is required");

        RuleFor(v => v.address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(ValidationConstants.Product.NameMaxLength).WithMessage($"Address should not exceed {ValidationConstants.Warehouse.AddressMaxLength} characters");

        RuleFor(v => v.name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(ValidationConstants.Product.SkuMaxLength).WithMessage($"SKU should not exceed {ValidationConstants.Warehouse.NameMaxLength} characters");
    }
}
