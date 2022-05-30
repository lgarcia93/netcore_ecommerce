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

    public async Task<Cart?> LoadCart(string userId)
    {
        return (await _context.Cart.ToListAsync()).FirstOrDefault(c => c.UserId == userId);
    }

    public async Task AddProduct(AddProductToCartModel cartProduct, string cartId)
    {
        var cart = new Cart { CartId = cartId };

        _context.Cart.Attach(cart);
        
        
        
        cart.Products.Add(cartProduct);

        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveProduct(string productId, string cartId)
    {
        var cart = new Cart { CartId = cartId };

        _context.Cart.Attach(cart);

        cart.Products.Remove(new CartProduct{ProductId = productId});

        await _context.SaveChangesAsync();
    }

    public async Task ClearCart(string cartId)
    {
        var cart = new Cart { CartId = new Guid() };
        
        _context.Cart.Attach(cart);

        _context.Remove(cart);
        
        await _context.SaveChangesAsync();
    }
}