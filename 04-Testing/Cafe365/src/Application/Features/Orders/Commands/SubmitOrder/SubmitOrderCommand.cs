using Ardalis.Specification.EntityFrameworkCore;
using Cafe365.Application.Common.Exceptions;
using Cafe365.Application.Common.Interfaces;
using Cafe365.Domain.Common.ValueObjects;
using Cafe365.Domain.Orders;
using Cafe365.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Cafe365.Application.Features.Orders.Commands.SubmitOrder;

public record SubmitOrderCommand(Payment Payment) : IRequest<Guid>
{
    [JsonIgnore]
    public Guid OrderId { get; set; }
}

public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, Guid>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IPaymentProvider _paymentProvider;

    public SubmitOrderCommandHandler(IApplicationDbContext applicationDbContext, IPaymentProvider paymentProvider)
    {
        _applicationDbContext = applicationDbContext;
        _paymentProvider = paymentProvider;
    }

    public async Task<Guid> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _applicationDbContext.Orders
            .WithSpecification(new OrderByIdSpec(new OrderId(request.OrderId)))
            .FirstAsync(cancellationToken);

        await ProcessPayment(request, order);
        await AdjustStock(order, cancellationToken);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return request.OrderId;
    }

    private async Task ProcessPayment(SubmitOrderCommand request, Order order)
    {
        var paymentResult = await _paymentProvider.ProcessPayment(request.Payment);
        if (!paymentResult.IsSuccessful)
            throw new PaymentFailedException("Payment was not successful");

        order.AddPayment(new Money(request.Payment.Currency, request.Payment.Amount));
    }

    private async Task AdjustStock(Order order, CancellationToken cancellationToken)
    {
        var productIds = order.Items.Select(i => i.ProductId).ToList();
        var products = await _applicationDbContext.Products
            .WithSpecification(new ProductByIdsSpec(productIds))
            .ToListAsync(cancellationToken);

        // Adjust stock levels
        foreach (var item in order.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId) ?? throw new ValidationException("Invalid Product");
            product.RemoveStock(item.Quantity);
        }
    }
}
