using CartService.Entity;
using CartService.Model;

namespace CartService.Repository;

public interface ICartRepository
{
    public Task<Cart?> LoadCart(string userId);

    public Task AddProduct(AddProductToCartModel cartProduct, string cartId);

    public Task RemoveProduct(string productId, string cartId);
    public Task ClearCart(string cartId);
}