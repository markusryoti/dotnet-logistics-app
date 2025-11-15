using Logistics.Shared;

namespace Logistics.Contracts.Orders;

public record OrderPlaced(Guid OrderId, Guid CustomerId, decimal Total) : IIntegrationEvent;