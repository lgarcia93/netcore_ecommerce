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
        var cartProducts = await _context.CartProduct.ToListAsync();
        var cartProductsFiltered = cartProducts.Where(c => c.UserId == userId);
        return cartProductsFiltered;
    }

    public async Task AddProduct(CartProduct cartProduct)
    {
        var product = _context.CartProduct.FirstOrDefault(c => c.ProductId == cartProduct.ProductId && c.UserId == cartProduct.UserId);

        if (product == null)
        {
            _context.CartProduct.Add(cartProduct);
    
            await _context.SaveChangesAsync();

            return;
        }

        product.Quantity = cartProduct.Quantity;

        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveProduct(string productId, string userId)
    {
        await _context.Database.ExecuteSqlRawAsync("Delete from CartProduct where ProductId = {0} AND UserId = {1}", productId, userId);
    }

    public async Task ClearCart(string userId)
    {
        await _context.Database.ExecuteSqlRawAsync("Delete from CartProduct where UserId = {0}", userId);
    }
}