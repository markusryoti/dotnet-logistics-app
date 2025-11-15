namespace Logistics.Shared;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

public interface IInProcessDomainEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IDomainEvent;
}

public class DomainEventBus(IServiceProvider sp, ILogger<DomainEventBus> log) : IInProcessDomainEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IDomainEvent
    {
        log.LogInformation("Publishing domain event {EventType}", typeof(TEvent).Name);

        using var scope = sp.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IDomainEventHandler<TEvent>>();
        foreach (var h in handlers)
        {
            try { await h.Handle(@event, token); }
            catch (Exception ex) { log.LogError(ex, "Error in handler {Handler}", h.GetType().Name); }
        }
    }
}

public interface IDomainEventsDispatcher { Task DispatchDomainEventsAsync(DbContext ctx, CancellationToken token = default); }

public class DomainEventsDispatcher(IInProcessDomainEventBus bus, ILogger<DomainEventsDispatcher> log) : IDomainEventsDispatcher
{
    public async Task DispatchDomainEventsAsync(DbContext ctx, CancellationToken token = default)
    {
        log.LogInformation("Dispatching domain events");

        var entities = ctx.ChangeTracker.Entries<IHasDomainEvents>().Where(e => e.Entity.DomainEvents?.Any() == true).ToArray();
        var events = entities.SelectMany(e => e.Entity.DomainEvents!).ToList();
        foreach (var e in entities) e.Entity.ClearDomainEvents();
        foreach (var ev in events)
        {
            await bus.PublishAsync((dynamic)ev, token);
        }
    }
}