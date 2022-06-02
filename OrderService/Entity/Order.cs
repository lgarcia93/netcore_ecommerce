using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entity;

public class Order
{
    [Column("Identifier")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption
        .Identity)]
    [Required]
    public int Identifier { get; set; }
    public string UserId { get; set; }
    [Timestamp]
    public DateTime OrderDate { get; set; }
    public IEnumerable<OrderProduct> OrderProducts { get; set; }
}