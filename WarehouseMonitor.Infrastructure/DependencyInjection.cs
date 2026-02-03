using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WarehouseMonitor.Application.Common.Interfaces;
using WarehouseMonitor.Infrastructure.Data;
using WarehouseMonitor.Infrastructure.Data.Interceptors;
using WarehouseMonitor.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace WarehouseMonitor.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("WarehouseMonitorDatabase");

        builder.Services.AddScoped<ISaveChangesInterceptor, AudiableEntityInterceptor>();
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });
        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<ApplicationDbContextInitialiser>();
        builder.Services.AddSingleton(TimeProvider.System);


        builder.Services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

    }
}
