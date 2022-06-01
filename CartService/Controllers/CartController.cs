using CartService.Model;
using CartService.Service;
using Core.Extension;
using Core.ServiceDiscovery;
using Core.ServiceDiscovery.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CartService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly IHttpClientFactory _httpClientFactory;
    
    public CartController(ICartService cartService,  IProductService productService, IHttpClientFactory httpClientFactory)
    {
        _cartService = cartService;
        _httpClientFactory = httpClientFactory;
        _productService = productService;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddProduct([FromBody] AddProductToCartModel product)
    {
        //make a call to a upstream service
        var productModel = await _productService.GetProductById(product.ProductId);

        var userId = HttpContext.GetUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var cartProduct = productModel.asCartProduct();
        cartProduct.UserId = userId;
        cartProduct.Quantity = product.Quantity;
        
        await _cartService.AddProduct(cartProduct);

        return Ok(cartProduct);
    }

    [HttpDelete("/{productId}")]
    [Authorize]
    public async Task<IActionResult> RemoveProduct(string productId)
    {
        var userId = HttpContext.GetUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        await _cartService.RemoveProduct(productId, userId);
        
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> CleanCart()
    {
        var userId = HttpContext.GetUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        await _cartService.ClearCart(userId);
        
        return Ok();
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCart()
    {
        var userId = HttpContext.GetUserId();

        if (userId == null)
        {
            return Unauthorized();
        }

        var cartProducts = await _cartService.LoadCart(userId);
        
        return Ok(cartProducts);
    }

}