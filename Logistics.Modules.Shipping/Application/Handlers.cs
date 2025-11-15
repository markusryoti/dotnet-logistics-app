using Logistics.Shared;
using Logistics.Contracts.Orders;

namespace LogisticsApp.Modules.Shipping;

internal class OrderPlacedIntegrationHandler(ILogger<OrderPlacedIntegrationHandler> logger) : IEventHandlerIntegration<OrderPlaced>
{
    public Task Handle(OrderPlaced @event, CancellationToken token)
    {
        logger.LogInformation("Received OrderPlaced integration event for OrderId: {OrderId}", @event.OrderId);
        return Task.CompletedTask;
    }
}