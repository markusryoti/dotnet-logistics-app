public class InventoryItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;

    public InventoryItem(string name)
    {
        Name = name;
    }
};