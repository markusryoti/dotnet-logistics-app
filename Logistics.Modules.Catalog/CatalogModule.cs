using Logistics.Modules.Catalog.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Modules.Orders;

public static class CatalogModule
{
    public static void RegisterServices(IServiceCollection services, IConfiguration config)
    {
    }

    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/catalog");

        Endpoints.MapCatalogEndpoints(group);
    }
}