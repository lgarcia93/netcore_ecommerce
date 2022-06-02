using Microsoft.EntityFrameworkCore;
using OrderService.Entity;

namespace OrderService.DbContext;

public sealed class DataContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
       Database.Migrate();
    }
     public DbSet<Order> Orders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Identifier);
        });
        
        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => e.OrderProductId);
        });
    }
}