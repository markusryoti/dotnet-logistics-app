using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Shipping;

public static class ShippingModule
{
    public static IServiceCollection AddShippingModule(this IServiceCollection services)
    {
        services.AddDbContext<ShippingDb>(opt => opt.UseInMemoryDatabase("Inventory"));

        return services;
    }

    public static RouteGroupBuilder MapShippingEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", () =>
        {
            var shipments = new List<Shipment>
            {
                new("Shipment 1"),
                new("Shipment 2"),
                new("Shipment 3")
            };

            return Results.Ok(shipments);
        });

        return group;
    }
}
