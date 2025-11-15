using Logistics.Contracts.Orders;
using Logistics.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Modules.Shipping.Application;

public class ShippingModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ShippingDb>();
        services.AddScoped<IEventHandlerIntegration<OrderPlaced>, OrderPlacedIntegrationHandler>();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/shipping");

        Endpoints.MapShippingEndpoints(group);
    }

    public void ApplyMigrations(IApplicationBuilder app)
    {
    }
}

