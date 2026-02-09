using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Application.ShipmentUnits.Queries;

public record GetShipmentUnitsQuery : IRequest<IEnumerable<ShipmentUnit>>;
public class GetShipmentUnitsRequestHandler : IRequestHandler<GetShipmentUnitsQuery, IEnumerable<ShipmentUnit>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetShipmentUnitsRequestHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ShipmentUnit>> Handle(GetShipmentUnitsQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.ShipmentUnits.ToListAsync(cancellationToken);
    }
}
