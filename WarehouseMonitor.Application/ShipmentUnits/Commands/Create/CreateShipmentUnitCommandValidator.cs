using FluentValidation;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Create;

public class CreateShipmentUnitCommandValidator
    : AbstractValidator<CreateShipmentUnitCommand>
{
    public CreateShipmentUnitCommandValidator()
    {
        RuleFor(x => x.trackingNumber)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.weight)
            .GreaterThan(0);

        RuleFor(x => x.volume)
            .GreaterThan(0);

        RuleFor(x => x.productId)
            .NotEmpty();
    }
}
