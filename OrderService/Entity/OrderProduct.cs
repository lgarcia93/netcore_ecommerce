using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entity;

public class OrderProduct
{
    [Column("OrderProductId")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption
        .Identity)]
    [Required]
    public int OrderProductId { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public Order Order { get; set; }
}