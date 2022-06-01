using CartService.Entity;
using CartService.Model;

namespace CartService.Service;

public interface ICartService
{
    public Task<IEnumerable<CartProduct>> LoadCart(string userId);

    public Task AddProduct(CartProduct cartProduct);

    public Task RemoveProduct(string productId, string cartId);
    public Task ClearCart(string cartId);
}