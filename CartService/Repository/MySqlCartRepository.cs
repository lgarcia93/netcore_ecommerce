using CartService.DbContext;
using CartService.Entity;
using CartService.Model;
using Microsoft.EntityFrameworkCore;

namespace CartService.Repository;

public class MySqlCartRepository : ICartRepository
{
    private readonly DataContext _context;

    public MySqlCartRepository(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<IEnumerable<CartProduct>> LoadCart(string userId)
    {
        return (await _context.CartProducts.ToListAsync()).Where(c => c.UserId == userId);
    }

    public async Task AddProduct(CartProduct cartProduct)
    {
        _context.CartProducts.Add(cartProduct);
    
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveProduct(string productId, string userId)
    {
        var cart = new CartProduct { ProductId = productId, UserId = userId};
        
        _context.Entry(cart);
        _context.Remove(cart);

        await _context.SaveChangesAsync();
    }

    public async Task ClearCart(string userId)
    {
        var cart = new CartProduct { UserId = userId};
        
        _context.Entry(cart);
        _context.Remove(cart);

        await _context.SaveChangesAsync();
    }
}