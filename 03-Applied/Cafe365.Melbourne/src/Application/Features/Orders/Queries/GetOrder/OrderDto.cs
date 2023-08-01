namespace Cafe365.Melbourne.Application.Features.Orders.Queries.GetOrder;

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