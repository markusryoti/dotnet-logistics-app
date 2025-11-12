using Microsoft.EntityFrameworkCore;

class ShippingDb : DbContext
{
    public ShippingDb(DbContextOptions<ShippingDb> options)
        : base(options) { }

    public DbSet<Shipment> Shipments => Set<Shipment>();
}