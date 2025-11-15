using Logistics.Shared;

namespace LogisticsApp.Modules.Orders.Domain;

public record OrderCreated(Guid Id, Guid CustomerId, decimal Total) : IDomainEvent;