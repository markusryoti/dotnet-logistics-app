using Microsoft.EntityFrameworkCore;

namespace LogisticsApp.Modules.Orders;

class OrderDb : DbContext
{
    public OrderDb(DbContextOptions<OrderDb> options)
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
}