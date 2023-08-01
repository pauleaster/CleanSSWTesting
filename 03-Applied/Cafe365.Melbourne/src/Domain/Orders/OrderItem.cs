using Cafe365.Melbourne.Domain.Common.Base;
using Cafe365.Melbourne.Domain.Common.ValueObjects;
using Cafe365.Melbourne.Domain.Products;

namespace Cafe365.Melbourne.Domain.Orders;

public record OrderItemId(Guid Value);

public class OrderItem : BaseEntity<OrderItemId>
{
    public OrderId OrderId { get; set; }
    public ProductId ProductId { get; set; }
    public int Quantity { get; set; }
    public Money Price { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
}
