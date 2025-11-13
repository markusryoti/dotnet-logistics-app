namespace LogisticsApp.Modules.Shipping;

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

