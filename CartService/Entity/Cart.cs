namespace CartService.Entity;

public class Cart
{
    public Guid CartId { get; set; }
    public string UserId { get; set; }

    public List<CartProduct> Products { get; set; }
}