using LogisticsApp.Modules.Orders.Events;
using LogisticsApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Shipping;

public static class ShippingModule
{
    public static IServiceCollection AddShippingModule(this IServiceCollection services)
    {
        services.AddDbContext<ShippingDb>(opt => opt.UseInMemoryDatabase("Inventory"));
        services.AddScoped<IEventHandlerIntegration<OrderPlaced>, OrderPlacedIntegrationHandler>();

        return services;
    }
}

