using Logistics.Modules.Catalog;
using Logistics.Modules.Inventory;
using Logistics.Modules.Orders;
using Logistics.Modules.Shipping.Application;

public static class ModuleLoader
{
    public static void RegisterModules(this IServiceCollection services, IConfiguration config, ILogger logger)
    {
        logger.LogInformation("Registering modules...");

        services.AddOrdersModule(config)
            .AddInventoryModule(config)
            .AddCatalogModule(config)
            .AddShippingModule(config);
    }

    public static void MapModuleEndpoints(this WebApplication app)
    {
        app.Logger.LogInformation("Mapping module endpoints...");

        OrdersModule.MapEndpoints(app);
        InventoryModule.MapEndpoints(app);
        CatalogModule.MapEndpoints(app);
        ShippingModule.MapEndpoints(app);
    }

    public static void ApplyModuleMigrations(this WebApplication app)
    {
        app.Logger.LogInformation("Applying module migrations...");

        OrdersModule.ApplyMigrations(app);
        InventoryModule.ApplyMigrations(app);
        CatalogModule.ApplyMigrations(app);
        ShippingModule.ApplyMigrations(app);
    }
}
