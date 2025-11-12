using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Inventory;

public static class InventoryModule
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services)
    {
        services.AddDbContext<InventoryDb>(opt => opt.UseInMemoryDatabase("Inventory"));

        return services;
    }

    public static RouteGroupBuilder MapInventoryEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", () =>
        {
            return Results.Ok(new
            {
                Message = "Inventory endpoint is working!"
            });
        });

        group.MapGet("/items", async (InventoryDb db) =>
        {
            var items = await db.Items.ToListAsync();

            return Results.Ok(items);
        });

        group.MapPost("/items", async (InventoryDb db, InventoryItem item) =>
        {
            db.Items.Add(item);
            await db.SaveChangesAsync();

            return Results.Created($"/api/inventory/items/{item.Id}", item);
        });

        return group;
    }
}

