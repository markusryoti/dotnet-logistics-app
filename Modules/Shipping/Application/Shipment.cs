class Shipment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;

    public Shipment(string name)
    {
        Name = name;
    }
};