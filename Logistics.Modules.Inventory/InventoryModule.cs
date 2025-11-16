using Logistics.Modules.Inventory.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Modules.Inventory;

public static class InventoryModule
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<InventoryDb>(opt => opt.UseInMemoryDatabase("Inventory"));
        // services.AddDbContext<InventoryDb>();
        services.AddScoped<IInventoryFacade, InventoryFacade>();

        return services;
    }

    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/inventory");

        Endpoints.MapInventoryEndpoints(group);
    }

    public static void ApplyMigrations(IApplicationBuilder app)
    {
        // using var scope = app.ApplicationServices.CreateScope();
        // var db = scope.ServiceProvider.GetRequiredService<InventoryDb>();
        // db.Database.Migrate();
    }
}

