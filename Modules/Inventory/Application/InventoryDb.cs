using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Inventory;

class InventoryDb : DbContext
{
    public InventoryDb(DbContextOptions<InventoryDb> options)
        : base(options) { }

    public DbSet<InventoryItem> Items => Set<InventoryItem>();
}