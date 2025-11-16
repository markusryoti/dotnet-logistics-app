using Logistics.Modules.Inventory;
using Logistics.Modules.Orders;
using Logistics.Modules.Shipping.Application;
using Logistics.Shared;

public static class ModuleLoader
{
    public static void RegisterModules(this IServiceCollection services, IConfiguration config, ILogger logger)
    {
        logger.LogInformation("Registering modules...");

        OrdersModule.RegisterServices(services, config);
        InventoryModule.RegisterServices(services, config);
        CatalogModule.RegisterServices(services, config);
        ShippingModule.RegisterServices(services, config);

        // var moduleTypes = AppDomain.CurrentDomain.GetAssemblies()
        //     .SelectMany(a => a.GetTypes())
        //     .Where(t => typeof(IModule).IsAssignableFrom(t) && t.IsClass);

        // logger.LogInformation("Found {ModuleCount} modules to register.", moduleTypes.Count());

        // foreach (var type in moduleTypes)
        // {
        //     logger.LogInformation("Registering module: {ModuleType}", type.FullName);
        //     var module = (IModule)Activator.CreateInstance(type)!;
        //     module.RegisterServices(services, config);
        // }
    }

    public static void MapModuleEndpoints(this WebApplication app)
    {
        app.Logger.LogInformation("Mapping module endpoints...");

        OrdersModule.MapEndpoints(app);
        InventoryModule.MapEndpoints(app);
        CatalogModule.MapEndpoints(app);
        ShippingModule.MapEndpoints(app);

        // var moduleTypes = AppDomain.CurrentDomain.GetAssemblies()
        //     .SelectMany(a => a.GetTypes())
        //     .Where(t => typeof(IModule).IsAssignableFrom(t) && t.IsClass);

        // app.Logger.LogInformation("Found {ModuleCount} modules to map endpoints for.", moduleTypes.Count());

        // foreach (var type in moduleTypes)
        // {
        //     app.Logger.LogInformation("Mapping endpoints for module: {ModuleType}", type.FullName);
        //     var module = (IModule)Activator.CreateInstance(type)!;
        //     module.MapEndpoints(app);
        // }
    }

    public static void ApplyModuleMigrations(this WebApplication app)
    {
        app.Logger.LogInformation("Applying module migrations...");

        OrdersModule.ApplyMigrations(app);
        // InventoryModule.ApplyMigrations(app);
        // CatalogModule.ApplyMigrations(app);
        // ShippingModule.ApplyMigrations(app);
    }
}
