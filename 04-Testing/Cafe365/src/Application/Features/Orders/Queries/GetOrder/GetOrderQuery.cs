using AutoMapper.QueryableExtensions;
using Cafe365.Application.Common.Exceptions;
using Cafe365.Application.Common.Interfaces;
using Cafe365.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Cafe365.Application.Features.Orders.Queries.GetOrder;

public record GetOrderQuery(Guid OrderId) : IRequest<OrderDto>;

public class OrderDto
{
    public required Guid OrderId { get; init; }
    public required string OrderStatus { get; init; }
    public required CustomerDto Customer { get; init; }
    public required List<OrderItemDto> Items { get; init; }
}

public class CustomerDto
{
    public required Guid CustomerId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}

public class OrderItemDto
{
    public required Guid OrderItemId { get; init; }
    public required int Quantity { get; init; }
    public required string Currency { get; init; }
    public required decimal Price { get; init; }
    public required ProductDto Product { get; init; }
}

public class ProductDto
{
    public required Guid ProductId { get; init; }
    public required string Sku { get; init; }
    public required string Name { get; init; }
}

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetOrderQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _applicationDbContext.Orders
            .Where(o => o.Id == new OrderId(request.OrderId))
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return order ?? throw new NotFoundException(nameof(Order), request.OrderId);
    }
}
