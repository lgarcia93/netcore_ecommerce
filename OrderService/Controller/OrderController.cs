using Core.Extension;
using Microsoft.AspNetCore.Mvc;
using OrderService.Entity;
using OrderService.Service;

namespace OrderService.Controller;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var userId = HttpContext.GetUserId();

        if (userId == null)
        {
            return Unauthorized();
        }
        
        var orders = await _orderService.GetOrders(userId);
        
        return Ok(orders);
    }
}