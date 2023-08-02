using Cafe365.Application.Common.Interfaces;
using Cafe365.Domain.Orders;
using Cafe365.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Cafe365.Application.Features.Orders.Commands.AddItem;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public AddItemCommandValidator(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;

        RuleFor(c => c.OrderId)
            .NotEmpty()
            .MustAsync(BeValidOrder).WithMessage("'{PropertyName}' must be valid");

        RuleFor(c => c.ProductId)
            .NotEmpty()
            .MustAsync(BeValidProduct).WithMessage("'{PropertyName}' must be valid");

        RuleFor(c => c.Quantity)
            .NotEmpty()
            .GreaterThan(0);
    }

    private async Task<bool> BeValidOrder(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await _applicationDbContext.Orders.FirstOrDefaultAsync(c => c.Id == new OrderId(orderId), cancellationToken);
        if (order == null)
            return false;

        if (order.OrderStatus is OrderStatus.Complete or OrderStatus.Cancelled)
            return false;

        return true;
    }

    private async Task<bool> BeValidProduct(Guid productId, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Products.AnyAsync(p => p.Id == new ProductId(productId), cancellationToken);
    }
}

