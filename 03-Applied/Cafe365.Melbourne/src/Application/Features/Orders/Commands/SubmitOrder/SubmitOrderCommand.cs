using Cafe365.Melbourne.Application.Common.Exceptions;
using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Domain.Common.ValueObjects;
using Cafe365.Melbourne.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Cafe365.Melbourne.Application.Features.Orders.Commands.SubmitOrder;

public record SubmitOrderCommand(Payment Payment) : IRequest<Guid>
{
    [JsonIgnore]
    public Guid OrderId { get; set; }
}


// 👇 updated code
public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    private readonly IPaymentProvider _paymentProvider;

    public SubmitOrderCommandHandler(IApplicationDbContext dbContext, IPaymentProvider paymentProvider)
    {
        _dbContext = dbContext;
        _paymentProvider = paymentProvider;
    }

    public async Task<Guid> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);

        var order = _dbContext.Orders
            .Include(o => o.Items)
            .First(o => o.Id == orderId);

        await ProcessPayment(request, order);
        await AdjustStock(order, cancellationToken);

        order.OrderStatus = OrderStatus.Complete;

        order.AddDomainEvent(new OrderSubmittedEvent(order));

        await _dbContext.SaveChangesAsync(cancellationToken);

        return order.Id.Value;
    }

    private async Task ProcessPayment(SubmitOrderCommand request, Order order)
    {
        if (order.Items == null || !order.Items.Any())
            throw new ValidationException("Cant submit an order without any items");

        if (order.OrderStatus is OrderStatus.Complete or OrderStatus.Cancelled)
            throw new ValidationException("Order status is invalid");

        if (order.PaidTotal.Amount == 0)
            order.PaidTotal = new Money(request.Payment.Currency, request.Payment.Amount);
        else
            order.PaidTotal = new Money(request.Payment.Currency, request.Payment.Amount + order.PaidTotal.Amount);

        if (order.PaidTotal.Amount >= order.PaidTotal.Amount)
            order.OrderStatus = OrderStatus.Complete;

        var orderTotal = order.Items.Sum(i => i.Price.Amount * i.Quantity);
        var paymentResult = await _paymentProvider.ProcessPayment(request.Payment);
        if (!paymentResult.IsSuccessful)
            throw new PaymentFailedException("Payment was not successful");
    }

    private async Task AdjustStock(Order order, CancellationToken cancellationToken) // 👈 updated code
    {
        var productIds = order.Items.Select(i => i.ProductId).ToList();
        var products = await _dbContext.Products.Where(p => productIds.Contains(p.Id)).ToListAsync(cancellationToken);

        foreach (var item in order.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId) ?? throw new ValidationException("Invalid Product");
            product.AvailableStock -= item.Quantity;

            if (product.IsStockedItem && product.AvailableStock < 0)
                throw new InvalidOperationException("Not enough stock submit the order");
        }
    }
}
// 👆 updated code
