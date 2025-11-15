using Logistics.Modules.Orders.Application;
using Logistics.Modules.Orders.Domain;
using Logistics.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Modules.Orders;

public static class OrdersModule
{
    public static void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Postgres");

        services.AddDbContext<OrderDb>(options =>
            options.UseNpgsql(connectionString));

        services.AddDbContext<OrderDb>();

        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IDomainEventHandler<OrderCreated>, OrderCreatedHandler>();

        services.AddScoped<IInProcessIntegrationEventBus, InProcessIntegrationEventBus>();
    }

    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/orders");

        Endpoints.MapOrdersEndpoints(group);
    }
}