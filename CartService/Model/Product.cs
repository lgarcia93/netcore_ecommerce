using CartService.Entity;

namespace CartService.Model;

public class Product
{
    public string? ProductId { get; set; }
 
    public string ProductName { get; set; }
 
    public string ProductDescription { get; set; }
   
    public float ProductPrice { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public CartProduct asCartProduct()
    {
        return new CartProduct
        {
            ProductId = ProductId,
            ProductDescription = ProductDescription,
            ProductName = ProductName,
            Quantity = 0
        };
    }
}