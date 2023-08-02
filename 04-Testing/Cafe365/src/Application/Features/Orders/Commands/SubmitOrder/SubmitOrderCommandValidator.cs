using Cafe365.Application.Common.Interfaces;
using Cafe365.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Cafe365.Application.Features.Orders.Commands.SubmitOrder;

public class SubmitOrderCommandValidator : AbstractValidator<SubmitOrderCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public SubmitOrderCommandValidator(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;

        RuleFor(c => c.OrderId)
            .NotEmpty()
            .MustAsync(BeValidOrder).WithMessage("'{PropertyName}' must be valid");

        RuleFor(c => c.Payment.CardNumber)
            .NotEmpty();

        RuleFor(c => c.Payment.CardName)
            .NotEmpty();

        RuleFor(c => c.Payment.CVV)
            .NotEmpty();

        RuleFor(c => c.Payment.ExpiryMonth)
            .NotEmpty();

        RuleFor(c => c.Payment.ExpiryYear)
            .NotEmpty();
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
}




