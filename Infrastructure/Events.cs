namespace LogisticsApp.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LogisticsApp.Shared;

public interface IInProcessBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IDomainEvent;
}

public class InProcessBus(IServiceProvider sp, ILogger<InProcessBus> log) : IInProcessBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IDomainEvent
    {
        log.LogInformation("Publishing event {EventType}", typeof(TEvent).Name);

        using var scope = sp.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
        foreach (var h in handlers)
        {
            try { await h.Handle(@event, token); }
            catch (Exception ex) { log.LogError(ex, "Error in handler {Handler}", h.GetType().Name); }
        }
    }
}

public interface IDomainEventsDispatcher { Task DispatchEventsAsync(DbContext ctx, CancellationToken token = default); }

public class DomainEventsDispatcher(IInProcessBus bus, ILogger<DomainEventsDispatcher> log) : IDomainEventsDispatcher
{
    public async Task DispatchEventsAsync(DbContext ctx, CancellationToken token = default)
    {
        log.LogInformation("Dispatching domain events");

        var entities = ctx.ChangeTracker.Entries<IHasDomainEvents>().Where(e => e.Entity.DomainEvents?.Any() == true).ToArray();
        var events = entities.SelectMany(e => e.Entity.DomainEvents!).ToList();
        foreach (var e in entities) e.Entity.ClearDomainEvents();
        // foreach (var ev in events) await bus.PublishAsync(ev, token);

        foreach (var ev in events)
        {
            await ((IInProcessBus)bus).PublishAsync((dynamic)ev, token);
        }
    }
}