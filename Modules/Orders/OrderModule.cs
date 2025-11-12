using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Orders;

public static class OrdersModule
{
    public static IServiceCollection AddOrdersModule(this IServiceCollection services)
    {
        services.AddDbContext<OrderDb>(opt => opt.UseInMemoryDatabase("Orders"));
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

        return group;
    }
}

