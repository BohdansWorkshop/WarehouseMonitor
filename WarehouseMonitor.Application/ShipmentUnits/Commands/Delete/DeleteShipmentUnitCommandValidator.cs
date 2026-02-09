using FluentValidation;

namespace WarehouseMonitor.Application.ShipmentUnits.Commands.Delete
{
    public class DeleteShipmentUnitCommandValidator : AbstractValidator<DeleteShipmentUnitCommand>
    {
        public DeleteShipmentUnitCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id must not be empty!");
        }
    }
}
