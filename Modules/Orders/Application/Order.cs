using LogisticsApp.Modules.Orders.Domain;
using LogisticsApp.Shared;

namespace LogisticsApp.Modules.Orders;

class Order : IHasDomainEvents
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public List<OrderItem> Items { get; private set; } = [];

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private List<IDomainEvent>? _events;
    public List<IDomainEvent>? DomainEvents => _events;

    private Order() { }

    public Order(List<OrderItem> items)
    {
        Items = items;
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

public class OrderItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CatalogItemId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public OrderItem(Guid catalogItemId, int quantity, decimal unitPrice)
    {
        CatalogItemId = catalogItemId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}