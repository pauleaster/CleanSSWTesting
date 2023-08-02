using Cafe365.Domain.Common.Base;
using Cafe365.Domain.Common.Exceptions;
using Cafe365.Domain.Common.ValueObjects;
using Cafe365.Domain.Customers;
using Cafe365.Domain.Products;
using System.ComponentModel.DataAnnotations;

namespace Cafe365.Domain.Orders;

public class Order : AggregateRoot<OrderId>
{
    public CustomerId CustomerId { get; private set; } = null!;

    public OrderStatus OrderStatus { get; private set; }

    public Customer Customer { get; private set; } = null!;

    private readonly List<OrderItem> _items = new();

    public IEnumerable<OrderItem> Items => _items.AsReadOnly();

    public Money OrderTotal
    {
        get
        {
            if (_items.Count == 0)
                return Money.Default;

            var amount = _items.Sum(li => li.Price.Amount * li.Quantity);
            var currency = _items.First().Price.Currency;

            return new Money(currency, amount);
        }
    }

    public Money PaidTotal { get; private set; } = null!;

    private Order() { }

    public static Order Create(CustomerId customerId)
    {
        Guard.Against.Null(customerId);

        var order = new Order()
        {
            Id = new OrderId(Guid.NewGuid()),
            CustomerId = customerId,
            OrderStatus = OrderStatus.New,
            PaidTotal = Money.Default
        };

        return order;
    }

    public OrderItem AddItem(Product product, int quantity)
    {
        Guard.Against.Null(product);

        var item = _items.FirstOrDefault(i => i.ProductId == product.Id);
        if (item != null)
            item.AddQuantity(quantity);
        else
        {
            item = OrderItem.Create(product, quantity);
            _items.Add(item);
        }

        OrderStatus = OrderStatus.InProgress;

        return item;
    }

    public void AddPayment(Money payment)
    {
        Guard.Against.Null(payment);
        Guard.Against.ZeroOrNegative(payment.Amount);

        if (_items.Count == 0)
            throw new ValidationException("Cant submit an order without any items");

        if (OrderStatus is OrderStatus.Complete or OrderStatus.Cancelled)
            throw new ValidationException("Order status is invalid");

        if (PaidTotal.IsZero)
            PaidTotal = payment;
        else
            PaidTotal += payment;

        if (PaidTotal >= OrderTotal)
        {
            OrderStatus = OrderStatus.Complete;
            AddDomainEvent(new OrderSubmittedEvent(this, Customer));
        }
    }
}

public record OrderId(Guid Value);
