namespace LogisticsApp.Shared;

public interface IDomainEvent { }

public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent @event, CancellationToken token);
}

public interface IIntegrationEvent { }

public interface IEventHandlerIntegration<TEvent> where TEvent : IIntegrationEvent
{
    Task Handle(TEvent @event, CancellationToken token);
}

public interface IHasDomainEvents
{
    List<IDomainEvent>? DomainEvents { get; }
    void AddDomainEvent(IDomainEvent ev);
    void ClearDomainEvents();
}