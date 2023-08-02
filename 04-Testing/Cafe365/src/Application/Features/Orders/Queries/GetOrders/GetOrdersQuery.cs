using Cafe365.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cafe365.Application.Features.Orders.Queries.GetOrders;

public record GetOrdersQuery() : IRequest<IReadOnlyList<OrderDto>>;

public record OrderDto(Guid OrderId, Guid CustomerId);

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IReadOnlyList<OrderDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetOrdersQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IReadOnlyList<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Orders
            .Select(o => new OrderDto(o.Id.Value, o.Customer.Id.Value))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}