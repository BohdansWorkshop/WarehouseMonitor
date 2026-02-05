using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Application.Products.Commands;
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

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return Ok(await _mediator.Send(new GetProductsQuery()));
    }

    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        await _mediator.Send(new CreateProductCommand(product.Name, product.SKU));
        return Ok(product);
    }
}