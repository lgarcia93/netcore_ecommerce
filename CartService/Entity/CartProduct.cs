namespace CartService.Entity;

public class CartProduct
{
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Quantity { get; set; }
}