using Logistics.Contracts.Orders;
using Logistics.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Modules.Shipping.Application;

public static class ShippingModule
{
    public static IServiceCollection AddShippingModule(this IServiceCollection services)
    {
        services.AddDbContext<ShippingDb>();
        services.AddScoped<IEventHandlerIntegration<OrderPlaced>, OrderPlacedIntegrationHandler>();

        return services;
    }
}

