using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Logistics.Modules.Catalog.Application;

public static class Endpoints
{
    public static RouteGroupBuilder MapCatalogEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", () =>
        {
            return Results.Ok(new { Message = "Catalog module is up and running!" });
        });

        return group;
    }
}

