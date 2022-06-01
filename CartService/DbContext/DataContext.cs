using CartService.Entity;
using Microsoft.EntityFrameworkCore;

namespace CartService.DbContext;

public sealed class DataContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
       Database.Migrate();
    }
    public DbSet<CartProduct> CartProduct { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity.HasKey(e => e.Identifier);
        });
    }
}