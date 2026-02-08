using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseMonitor.Application.Warehouses.Commands.Create;
using WarehouseMonitor.Application.Warehouses.Commands.Delete;
using WarehouseMonitor.Application.Warehouses.Commands.Update;
using WarehouseMonitor.Application.Warehouses.Queries;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly ISender _mediator;

    public WarehouseController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
    {
        return Ok(await _mediator.Send(new GetWarehousesQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateWarehouse(Warehouse warehouse)
    {
        var result = await _mediator.Send(new CreateWarehouseCommand(warehouse.Name, warehouse.BranchCode, warehouse.Address));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateWarehouse(Guid id, Warehouse warehouse)
    {
        if (id != warehouse.Id)
        {
            return BadRequest("Route Id and Warehouse.Id do not match.");
        }

        var result = await _mediator.Send(new UpdateWarehouseCommand(warehouse));
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteWarehouse(Guid id)
    {
        var result = await _mediator.Send(new DeleteWarehouseCommand(id));
        return result ? NoContent() : NotFound();
    }
}
