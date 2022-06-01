using CartService.Entity;
using Microsoft.EntityFrameworkCore;

namespace CartService.DbContext;

public class DataContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    public DbSet<CartProduct> CartProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId);
            entity.Property(e => e.UserId).IsRequired();
            entity.HasMany(e => e.Products);
        });

        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId);
        });
    }
}