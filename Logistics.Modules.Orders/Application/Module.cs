using LogisticsApp.Infrastructure;
using LogisticsApp.Modules.Inventory.Api;
using LogisticsApp.Modules.Orders.Domain;
using LogisticsApp.Shared;

namespace LogisticsApp.Modules.Orders;

public static class OrdersModule
{
    public static IServiceCollection AddOrdersModule(this IServiceCollection services)
    {
        services.AddDbContext<OrderDb>();

        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IDomainEventHandler<OrderCreated>, OrderCreatedHandler>();

        services.AddScoped<IInProcessIntegrationEventBus, InProcessIntegrationEventBus>();

        services.AddScoped<IInventoryFacade, InventoryFacade>();

        return services;
    }
}

