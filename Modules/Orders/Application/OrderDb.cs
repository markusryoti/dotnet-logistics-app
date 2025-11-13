using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Orders;

class OrderDb : DbContext
{
    public OrderDb(DbContextOptions<OrderDb> options)
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 👇 Important: set schema per module
        modelBuilder.HasDefaultSchema("orders");

        // Map Order aggregate
        modelBuilder.Entity<Order>(order =>
        {
            order.ToTable("Orders");

            order.HasKey(o => o.Id);
            order.Property(o => o.CreatedAt).IsRequired();

            // 👇 Ignore domain events so EF doesn’t try to persist them
            order.Ignore(o => o.DomainEvents);

            // 👇 Configure relationship with OrderItem (one-to-many)
            order.OwnsMany(o => o.Items, item =>
            {
                item.ToTable("OrderItems");
                item.WithOwner().HasForeignKey("OrderId");
                item.HasKey(i => i.Id);

                item.Property(i => i.CatalogItemId).IsRequired();
                item.Property(i => i.Quantity).IsRequired();
                item.Property(i => i.UnitPrice).HasPrecision(10, 2);
            });
        });
    }
}