using Logistics.Modules.Orders.Domain;
using Logistics.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Logistics.Modules.Orders.Application;

public static class Endpoints
{
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

            await dispatcher.DispatchDomainEventsAsync(db);

            return Results.Created($"/api/orders/{order.Id}", order);
        });


        return group;
    }
}
