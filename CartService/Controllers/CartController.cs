using CartService.Model;
using CartService.Service;
using Core.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddProduct([FromBody] AddProductToCartModel product)
    {
        
        
        await _cartService.AddProduct(product);
        return Ok();
    }

    [HttpDelete("/{productId}")]
    [Authorize]
    public async Task<IActionResult> RemoveProduct(string productId)
    {
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCart()
    {
        HttpContext.GetUserId();
        return Ok();
    }

}