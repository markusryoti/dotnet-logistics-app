using LogisticsApp.Infrastructure;
using LogisticsApp.Modules.Inventory.Api;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Orders;

public static class OrdersModule
{
    public static IServiceCollection AddOrdersModule(this IServiceCollection services)
    {
        services.AddDbContext<OrderDb>(opt => opt.UseInMemoryDatabase("Orders"));
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

        services.AddScoped<IInventoryFacade, InventoryFacade>();

        return services;
    }

    public static RouteGroupBuilder MapOrdersEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (OrderDb db) =>
        {
            var orders = await db.Orders.ToListAsync();

            return Results.Ok(orders);
        });

        group.MapPost("/", async (Order order, OrderDb db, IInventoryFacade inventory, IDomainEventsDispatcher dispatcher, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("OrdersModule");

            var inStock = await inventory.IsInStockAsync(order.Id, 1, CancellationToken.None);

            logger.LogInformation("Checking stock for OrderId: {OrderId}, InStock: {InStock}", order.Id, inStock);

            db.Orders.Add(order);
            await db.SaveChangesAsync();
            await dispatcher.DispatchEventsAsync(db);
            return Results.Created($"/api/orders/{order.Id}", order);
        });


        return group;
    }
}

