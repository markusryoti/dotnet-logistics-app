using Logistics.Modules.Inventory.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Logistics.Shared;

namespace Logistics.Modules.Inventory;

public class InventoryModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<InventoryDb>(opt => opt.UseInMemoryDatabase("Inventory"));
        // services.AddDbContext<InventoryDb>();
        services.AddScoped<IInventoryFacade, InventoryFacade>();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/inventory");

        Endpoints.MapInventoryEndpoints(group);
    }

    public void ApplyMigrations(IApplicationBuilder app)
    {
    }
}

