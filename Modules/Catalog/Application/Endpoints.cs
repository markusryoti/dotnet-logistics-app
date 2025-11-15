namespace LogisticsApp.Modules.Catalog;

public static class Endpoints
{

    public static RouteGroupBuilder MapCatalogEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", () =>
        {

            return Results.Ok(new
            {
                Message = "Catalog endpoint is working!"
            });
        });

        return group;
    }
}

