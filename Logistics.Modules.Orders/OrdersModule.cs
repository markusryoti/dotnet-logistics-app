using Logistics.Shared;
using Logistics.Modules.Orders.Domain;
using Logistics.Modules.Orders.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Logistics.Modules.Orders.Infrastructure;

namespace Logistics.Modules.Orders;

public class OrdersModule : IModule
{
    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Postgres");

        services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddDbContext<OrderDbContext>();

        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IDomainEventHandler<OrderCreated>, OrderCreatedHandler>();

        services.AddScoped<IInProcessIntegrationEventBus, InProcessIntegrationEventBus>();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/orders");

        Endpoints.MapOrdersEndpoints(group);
    }

    public void ApplyMigrations(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        db.Database.Migrate();
    }
}