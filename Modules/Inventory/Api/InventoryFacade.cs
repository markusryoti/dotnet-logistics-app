using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Inventory.Api;

interface IInventoryFacade
{
    Task<bool> IsInStockAsync(Guid productId, int quantity, CancellationToken token);
}

class InventoryFacade : IInventoryFacade
{
    private readonly InventoryDb db;

    public InventoryFacade(InventoryDb db)
    {
        this.db = db;
    }

    public async Task<bool> IsInStockAsync(Guid productId, int quantity, CancellationToken token)
    {
        var stock = await db.Items.FirstOrDefaultAsync(s => s.Id == productId, token);
        return stock != null && stock.Quantity >= quantity;
    }
}
