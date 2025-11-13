using LogisticsApp.Modules.Inventory.Api;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Inventory;

public static class InventoryModule
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services)
    {
        services.AddDbContext<InventoryDb>(opt => opt.UseInMemoryDatabase("Inventory"));
        services.AddScoped<IInventoryFacade, InventoryFacade>();

        return services;
    }
}

