using Microsoft.EntityFrameworkCore;

namespace Logistics.Modules.Inventory.Application;

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
