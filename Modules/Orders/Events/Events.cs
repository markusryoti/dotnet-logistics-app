using LogisticsApp.Shared;

namespace LogisticsApp.Modules.Orders.Events;

public record OrderPlaced(Guid OrderId, Guid CustomerId, decimal Total) : IDomainEvent;