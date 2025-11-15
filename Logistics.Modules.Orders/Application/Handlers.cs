using Logistics.Contracts.Orders;
using Logistics.Modules.Orders.Domain;
using Logistics.Shared;
using Microsoft.Extensions.Logging;

namespace Logistics.Modules.Orders.Application;

class OrderCreatedHandler : IDomainEventHandler<OrderCreated>
{
    private readonly ILogger<OrderCreatedHandler> _logger;
    private readonly IInProcessIntegrationEventBus _bus;

    public OrderCreatedHandler(ILogger<OrderCreatedHandler> logger, IInProcessIntegrationEventBus bus)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task Handle(OrderCreated @event, CancellationToken token)
    {
        _logger.LogInformation("Handling OrderPlaced domain event for OrderId: {OrderId}", @event.Id);

        var integrationEvent = new OrderPlaced(CustomerId: @event.CustomerId, OrderId: @event.Id, Total: @event.Total);

        await _bus.PublishAsync((dynamic)integrationEvent, token);
    }
}