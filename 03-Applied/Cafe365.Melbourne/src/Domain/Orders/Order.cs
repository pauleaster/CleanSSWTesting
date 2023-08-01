using Cafe365.Melbourne.Domain.Common.Base;
using Cafe365.Melbourne.Domain.Common.ValueObjects;
using Cafe365.Melbourne.Domain.Customers;

namespace Cafe365.Melbourne.Domain.Orders;

public record OrderId(Guid Value);

public class Order : BaseEntity<OrderId>
{
    public required CustomerId CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Money PaidTotal { get; set; }

    public Customer? Customer { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
