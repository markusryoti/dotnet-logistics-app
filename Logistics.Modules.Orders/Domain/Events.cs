using Logistics.Shared;

namespace Logistics.Modules.Orders.Domain;

public record OrderCreated(Guid Id, Guid CustomerId, decimal Total) : IDomainEvent;