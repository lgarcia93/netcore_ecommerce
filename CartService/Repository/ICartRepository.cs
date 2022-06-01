using CartService.Entity;
using CartService.Model;

namespace CartService.Repository;

public interface ICartRepository
{
    public Task<IEnumerable<CartProduct>> LoadCart(string userId);

    public Task AddProduct(CartProduct cartProduct);

    public Task RemoveProduct(string productId,  string userId);
    public Task ClearCart(string userId);
}