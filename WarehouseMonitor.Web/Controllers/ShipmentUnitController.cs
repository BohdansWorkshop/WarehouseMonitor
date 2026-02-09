using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseMonitor.Application.ShipmentUnits.Commands.Create;
using WarehouseMonitor.Application.ShipmentUnits.Commands.Update;
using WarehouseMonitor.Application.ShipmentUnits.Commands.Delete;
using WarehouseMonitor.Application.ShipmentUnits.Queries;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentUnitsController : ControllerBase
{
    private readonly ISender _mediator;

    public ShipmentUnitsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipmentUnit>>> GetShipmentUnits()
    {
        var units = await _mediator.Send(new GetShipmentUnitsQuery());
        return Ok(units);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateShipmentUnit(CreateShipmentUnitCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<bool>> UpdateShipmentUnit(Guid id, UpdateShipmentUnitCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route Id and command.Id do not match.");

        var isUpdated = await _mediator.Send(command);
        return isUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteShipmentUnit(Guid id)
    {
        var isDeleted = await _mediator.Send(new DeleteShipmentUnitCommand(id));
        return isDeleted ? NoContent() : NotFound();
    }
}
