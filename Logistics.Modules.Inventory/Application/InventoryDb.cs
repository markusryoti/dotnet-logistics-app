using Microsoft.EntityFrameworkCore;

namespace Logistics.Modules.Inventory.Application;

class InventoryDb : DbContext
{
    public InventoryDb(DbContextOptions<InventoryDb> options)
        : base(options) { }

    public DbSet<InventoryItem> Items => Set<InventoryItem>();
}