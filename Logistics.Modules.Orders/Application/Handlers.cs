using Logistics.Contracts.Orders;
using Logistics.Shared;
using LogisticsApp.Modules.Orders.Domain;

class OrderCreatedHandler(ILogger<OrderCreatedHandler> logger, IInProcessIntegrationEventBus bus) : IDomainEventHandler<OrderCreated>
{
    public async Task Handle(OrderCreated @event, CancellationToken token)
    {
        logger.LogInformation("Handling OrderPlaced domain event for OrderId: {OrderId}", @event.Id);

        var integrationEvent = new OrderPlaced(CustomerId: @event.CustomerId, OrderId: @event.Id, Total: @event.Total);

        await bus.PublishAsync((dynamic)integrationEvent, token);
    }
}