class InventoryItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;
    public int Quantity { get; private set; } = 0;

    public InventoryItem(string name)
    {
        Name = name;
    }
};