using FluentValidation;
using WarehouseMonitor.Domain.Constants;

namespace WarehouseMonitor.Application.Products.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(ValidationConstants.Product.NameMaxLength).WithMessage($"Name should not exceed {ValidationConstants.Product.NameMaxLength} characters");

        RuleFor(v => v.Sku)
            .NotEmpty().WithMessage("SKU is required")
            .MaximumLength(ValidationConstants.Product.SkuMaxLength).WithMessage($"SKU should not exceed {ValidationConstants.Product.SkuMaxLength} characters");
    }
}
