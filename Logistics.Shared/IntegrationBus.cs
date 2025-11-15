namespace Logistics.Shared;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public interface IInProcessIntegrationEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IIntegrationEvent;
}

public class InProcessIntegrationEventBus : IInProcessIntegrationEventBus
{
    private readonly IServiceProvider _sp;
    private readonly ILogger<InProcessIntegrationEventBus> _log;

    public InProcessIntegrationEventBus(IServiceProvider sp, ILogger<InProcessIntegrationEventBus> log)
    {
        _sp = sp ?? throw new ArgumentNullException(nameof(sp));
        _log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default) where TEvent : IIntegrationEvent
    {
        _log.LogInformation("Publishing integration event {EventType}", typeof(TEvent).Name);

        using var scope = _sp.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandlerIntegration<TEvent>>();
        foreach (var h in handlers)
        {
            try { await h.Handle(@event, token); }
            catch (Exception ex) { _log.LogError(ex, "Error in handler {Handler}", h.GetType().Name); }
        }
    }
}
