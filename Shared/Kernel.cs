namespace LogisticsApp.Shared;

public interface IDomainEvent { }

public interface IEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent @event, CancellationToken token);
}

public interface IHasDomainEvents
{
    List<IDomainEvent>? DomainEvents { get; }
    void AddDomainEvent(IDomainEvent ev);
    void ClearDomainEvents();
}