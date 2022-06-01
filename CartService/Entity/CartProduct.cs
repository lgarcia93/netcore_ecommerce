using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartService.Entity;

public class CartProduct
{
    [Column("Identifier")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption
        .Identity)]
    [Required]
    public int Identifier { get; set; }
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Quantity { get; set; }
}