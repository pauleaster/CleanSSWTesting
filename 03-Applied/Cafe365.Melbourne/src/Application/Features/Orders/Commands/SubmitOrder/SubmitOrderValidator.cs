using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Cafe365.Melbourne.Application.Features.Orders.Commands.SubmitOrder;

public class SubmitOrderCommandValidator : AbstractValidator<SubmitOrderCommand>
{
    // 👇 updated code
    private readonly IApplicationDbContext _dbContext;

    public SubmitOrderCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

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
        var order = await _dbContext.Orders.FirstOrDefaultAsync(c => c.Id == new OrderId(orderId), cancellationToken);
        if (order == null)
            return false;

        if (order.OrderStatus is OrderStatus.Complete or OrderStatus.Cancelled)
            return false;

        return true;
    }
    // 👆 updated code
}