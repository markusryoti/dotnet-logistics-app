using Logistics.Modules.Catalog.Application;
using Logistics.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Modules.Orders;

public class CatalogModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/catalog");

        Endpoints.MapCatalogEndpoints(group);
    }

    public void ApplyMigrations(IApplicationBuilder app)
    {
    }
}