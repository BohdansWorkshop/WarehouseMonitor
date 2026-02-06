using FluentValidation;
using WarehouseMonitor.Domain.Constants;

namespace WarehouseMonitor.Application.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.product.Id).NotEmpty().WithMessage("Id must not be empty!");
            RuleFor(x => x.product.Description).NotEmpty().WithMessage("Description must not be empty!");
            RuleFor(x => x.product.Name).NotEmpty().WithMessage("Name must not be empty!")
            .MaximumLength(ValidationConstants.Product.NameMaxLength).WithMessage($"Name should not exceed {ValidationConstants.Product.NameMaxLength} characters");
            RuleFor(x => x.product.SKU).NotEmpty().WithMessage("Sku must not be empty!")
            .MaximumLength(ValidationConstants.Product.SkuMaxLength).WithMessage($"SKU should not exceed {ValidationConstants.Product.SkuMaxLength} characters");
            RuleFor(x => x.product.Type).IsInEnum().WithMessage("Type must not be empty!");
        }
    }
}
