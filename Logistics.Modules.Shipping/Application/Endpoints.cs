using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Logistics.Modules.Shipping.Application;

public static class Endpoints
{
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

