public interface IInventoryFacade
{
    Task<bool> IsInStockAsync(Guid productId, int quantity, CancellationToken token);
}