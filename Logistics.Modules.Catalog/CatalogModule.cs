using Logistics.Modules.Catalog.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Modules.Orders;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }

    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/catalog");

        Endpoints.MapCatalogEndpoints(group);
    }

    public static void ApplyMigrations(IApplicationBuilder app)
    {
        // using var scope = app.ApplicationServices.CreateScope();
        // var db = scope.ServiceProvider.GetRequiredService<CatalogDb>();
        // db.Database.Migrate();
    }
}