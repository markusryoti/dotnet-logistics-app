using LogisticsApp.Modules.Orders.Domain;
using LogisticsApp.Shared;

namespace LogisticsApp.Modules.Orders;

class Order : IHasDomainEvents
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;

    private List<IDomainEvent>? _events;
    public List<IDomainEvent>? DomainEvents => _events;

    public Order(string name)
    {
        Name = name;
        AddDomainEvent(new OrderCreated(Id, Guid.NewGuid(), 100.0m));
    }

    public void AddDomainEvent(IDomainEvent ev)
    {
        _events ??= []; _events.Add(ev);
    }

    public void ClearDomainEvents()
    {
        _events?.Clear();
    }
};