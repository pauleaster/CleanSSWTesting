using Ardalis.Specification;

namespace Cafe365.Domain.Orders;

public sealed class OrderByIdSpec : SingleResultSpecification<Order>
{
    public OrderByIdSpec(OrderId orderId)
    {
        Query
            .Where(o => o.Id == orderId)
            .Include(o => o.Items);
    }
}