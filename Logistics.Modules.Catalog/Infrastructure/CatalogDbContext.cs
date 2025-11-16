using Logistics.Modules.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Modules.Catalog.Infrastructure;

class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    public DbSet<CatalogItem> CatalogItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 👇 Important: set schema per module
        modelBuilder.HasDefaultSchema("catalog");

        // Map CatalogItem entity
        modelBuilder.Entity<CatalogItem>(item =>
        {
            item.ToTable("CatalogItems");

            item.HasKey(i => i.Id);
            item.Property(i => i.Name).IsRequired().HasMaxLength(200);
            item.Property(i => i.Description).HasMaxLength(1000);
            item.Property(i => i.Price).HasPrecision(10, 2).IsRequired();
            item.Property(i => i.CreatedAt).IsRequired();
        });
    }
}