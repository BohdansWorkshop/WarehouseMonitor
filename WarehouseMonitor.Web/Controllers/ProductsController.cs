using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseMonitor.Application.Products.Commands.Create;
using WarehouseMonitor.Application.Products.Commands.Delete;
using WarehouseMonitor.Application.Products.Commands.Update;
using WarehouseMonitor.Application.Products.Queries;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _mediator;

    public ProductsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return Ok(await _mediator.Send(new GetProductsQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProduct(Product product)
    {
        var id = await _mediator.Send(new CreateProductCommand(product.Name, product.SKU));
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateProduct(Guid id, Product product)
    {
        if(id != product.Id)
        {
            return BadRequest("Route Id and Product.Id do not match.");
        }
        var isUpdated = await _mediator.Send(new UpdateProductCommand(product));
        return isUpdated ? NoContent() : BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var isDeleted = await _mediator.Send(new DeleteProductCommand(id));
        return isDeleted ? NoContent() : NotFound();
    }
}