namespace Logistics.Shared;

public interface IInProcessIntegrationEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IIntegrationEvent;
}

public class InProcessIntegrationEventBus(IServiceProvider sp, ILogger<DomainEventBus> log) : IInProcessIntegrationEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IIntegrationEvent
    {
        log.LogInformation("Publishing integration event {EventType}", typeof(TEvent).Name);

        using var scope = sp.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandlerIntegration<TEvent>>();
        foreach (var h in handlers)
        {
            try { await h.Handle(@event, token); }
            catch (Exception ex) { log.LogError(ex, "Error in handler {Handler}", h.GetType().Name); }
        }
    }
}
