public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;

    public Order(string name)
    {
        Name = name;
    }
};