using Cafe365.Melbourne.Domain.Common.Base;
using Cafe365.Melbourne.Domain.Common.Exceptions;
using Cafe365.Melbourne.Domain.Common.ValueObjects;
using Cafe365.Melbourne.Domain.Products;

namespace Cafe365.Melbourne.Domain.Orders;

public record OrderItemId(Guid Value);

// 👇 updated code
public class OrderItem : Entity<OrderItemId>
{
    public OrderId OrderId { get; private set; } = null!;
    public ProductId ProductId { get; private set; } = null!;
    public int Quantity { get; private set; }
    public Money Price { get; private set; } = null!;

    public Order? Order { get; private set; }
    public Product? Product { get; private set; }

    private OrderItem() { }

    internal static OrderItem Create(Product product, int quantity)
    {
        Guard.Against.Null(product.Price);
        Guard.Against.ZeroOrNegative(product.Price.Amount);

        return new OrderItem()
        {
            Id = new OrderItemId(Guid.NewGuid()),
            ProductId = product.Id,
            Price = product.Price,
            Quantity = quantity
        };
    }

    public void AddQuantity(int quantity)
    {
        Guard.Against.ZeroOrNegative(quantity);
        Quantity += quantity;
    }

    public void RemoveQuantity(int quantity)
    {
        Guard.Against.ZeroOrNegative(quantity);
        Quantity -= quantity;
    }
}
// 👆 update code
