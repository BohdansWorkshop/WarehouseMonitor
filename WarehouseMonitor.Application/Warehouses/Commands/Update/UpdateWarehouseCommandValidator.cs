using FluentValidation;
using WarehouseMonitor.Domain.Constants;

namespace WarehouseMonitor.Application.Warehouses.Commands.Update;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    public UpdateWarehouseCommandValidator()
    {
        RuleFor(x => x.warehouse.Id).NotEmpty().WithMessage("Id must not be empty!");
        RuleFor(x => x.warehouse.Name).NotEmpty().WithMessage("Name must not be empty!")
        .MaximumLength(ValidationConstants.Warehouse.NameMaxLength).WithMessage($"Name should not exceed {ValidationConstants.Warehouse.NameMaxLength} characters");
        RuleFor(x => x.warehouse.Address).NotEmpty().WithMessage("Address must not be empty!")
        .MaximumLength(ValidationConstants.Warehouse.AddressMaxLength).WithMessage($"Address should not exceed {ValidationConstants.Warehouse.AddressMaxLength} characters");
        RuleFor(x => x.warehouse.BranchCode).NotEmpty().WithMessage("BranchCode must not be empty!");
    }
}