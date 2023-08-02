using Cafe365.Application.Features.Customers.Queries.GetCustomers;
using Cafe365.Application.Features.Orders.Queries.GetOrders;
using MediatR;

namespace GraphQL.Queries;

public class Query
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<CustomerDto>> GetCustomers([Service(ServiceKind.Synchronized)] ISender sender) => await sender.Send(new GetCustomersQuery());

    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public async Task<IReadOnlyList<OrderDto>> GetOrders([Service(ServiceKind.Synchronized)] ISender sender) => await sender.Send(new GetOrdersQuery());
}
