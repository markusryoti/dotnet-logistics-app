using LogisticsApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Orders;

public static class OrdersModule
{
    public static IServiceCollection AddOrdersModule(this IServiceCollection services)
    {
        services.AddDbContext<OrderDb>(opt => opt.UseInMemoryDatabase("Orders"));
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        return services;
    }

    public static RouteGroupBuilder MapOrdersEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", () =>
        {
            var orders = new List<Order>
            {
                new("Order 1"),
                new("Order 2"),
                new("Order 3")
            };

            return Results.Ok(orders);
        });

        group.MapPost("/", async (Order order, OrderDb db, IDomainEventsDispatcher dispatcher) =>
        {
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            await dispatcher.DispatchEventsAsync(db);
            return Results.Created($"/api/orders/{order.Id}", order);
        });

        return group;
    }
}

