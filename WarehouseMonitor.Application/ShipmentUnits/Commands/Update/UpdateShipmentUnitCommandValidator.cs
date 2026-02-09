using FluentValidation;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Update;

public class UpdateShipmentUnitCommandValidator
    : AbstractValidator<UpdateShipmentUnitCommand>
{
    public UpdateShipmentUnitCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.TrackingNumber)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.Weight)
            .GreaterThan(0);

        RuleFor(x => x.Volume)
            .GreaterThan(0);

        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}
