namespace LogisticsApp.Shared.Events;

public record OrderPlaced(Guid OrderId, Guid CustomerId, decimal Total) : IDomainEvent;