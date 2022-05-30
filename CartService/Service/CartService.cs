using CartService.Entity;
using CartService.Model;
using CartService.Repository;

namespace CartService.Service;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    public CartService(ICartRepository repository)
    {
        _cartRepository = repository;
    }

    public Task<Cart?> LoadCart(string userId)
    {
        return _cartRepository.LoadCart(userId);
    }

    public Task AddProduct(AddProductToCartModel cartProduct, string cartId)
    {
        return _cartRepository.AddProduct(cartProduct, cartId);
    }

    public Task RemoveProduct(string productId, string cartId)
    {
        return _cartRepository.RemoveProduct(productId, cartId);
    }

    public Task ClearCart(string cartId)
    {
        return _cartRepository.ClearCart(cartId);
    }
}