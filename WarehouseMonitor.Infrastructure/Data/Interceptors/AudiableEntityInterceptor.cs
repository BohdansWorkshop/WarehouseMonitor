using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using WarehouseMonitor.Domain.Common;

namespace WarehouseMonitor.Infrastructure.Data.Interceptors;

public class AudiableEntityInterceptor : SaveChangesInterceptor
{
    private readonly TimeProvider _dateTime;

    public AudiableEntityInterceptor(TimeProvider dateTime)
    {
        _dateTime = dateTime;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        this.UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null)
            return;

        foreach(var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if(entry.State is EntityState.Added or EntityState.Modified)
            {
                var utcNow = _dateTime.GetUtcNow();
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.Created = utcNow;
                }

                entry.Entity.LastModified = utcNow;
            }
        }

    }
}
