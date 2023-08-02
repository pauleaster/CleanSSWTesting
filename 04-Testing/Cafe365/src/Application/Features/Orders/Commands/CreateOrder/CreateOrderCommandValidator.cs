using Cafe365.Application.Common.Interfaces;
using Cafe365.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Cafe365.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateOrderCommandValidator(IApplicationDbContext applicationDbContext)
    {
        RuleFor(c => c.CustomerId)
            .NotEmpty()
            .MustAsync(Exist).WithMessage("'{PropertyName}' must be valid");

        _applicationDbContext = applicationDbContext;
    }

    private async Task<bool> Exist(Guid customerId, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Customers.AnyAsync(c => c.Id == new CustomerId(customerId), cancellationToken);
    }
}

