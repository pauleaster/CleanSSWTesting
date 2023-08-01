using Microsoft.EntityFrameworkCore;
using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Domain.Orders;
using AutoMapper.QueryableExtensions;
using Cafe365.Melbourne.Application.Common.Exceptions;

namespace Cafe365.Melbourne.Application.Features.Orders.Queries.GetOrder;

public record GetOrderQuery(Guid OrderId) : IRequest<OrderDto>;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetOrderQueryHandler(
        IMapper mapper,
        IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<OrderDto> Handle(
        GetOrderQuery request,
        CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);

        var order = await _dbContext.Orders
            .Where(o => o.Id == orderId)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return order ?? throw new NotFoundException(nameof(Order), request.OrderId);

    }
}