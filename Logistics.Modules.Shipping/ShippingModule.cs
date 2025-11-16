using Logistics.Contracts.Orders;
using Logistics.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Modules.Shipping.Application;

public static class ShippingModule
{
    public static IServiceCollection AddShippingModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ShippingDb>();
        services.AddScoped<IEventHandlerIntegration<OrderPlaced>, OrderPlacedIntegrationHandler>();

        return services;
    }

    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/shipping");

        Endpoints.MapShippingEndpoints(group);
    }

    public static void ApplyMigrations(IApplicationBuilder app)
    {
        // using var scope = app.ApplicationServices.CreateScope();
        // var db = scope.ServiceProvider.GetRequiredService<ShippingDb>();
        // db.Database.Migrate();
    }
}

