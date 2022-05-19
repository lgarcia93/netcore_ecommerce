using Microsoft.AspNetCore.Mvc;
using ProductService.Domain;
using ProductService.Services;

namespace ProductService.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet("{productId}")]
    public async Task<IActionResult> Get(string productId)
    {
        var product = await _productService.GetProductById(productId);

        return Ok(product);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetProducts();

        return Ok(products);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        var createdProduct = await _productService.CreateProduct(product);

        return Ok(createdProduct);
    }
    
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(string productId)
    {
        await _productService.DeleteProduct(productId);

        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        var updatedProduct = await _productService.UpdateProduct(product);

        return Ok(updatedProduct);
    }
}