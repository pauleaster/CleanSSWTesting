using Ardalis.Specification.EntityFrameworkCore;
using Cafe365.Application.Common.Interfaces;
using Cafe365.Domain.Orders;
using Cafe365.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Cafe365.Application.Features.Orders.Commands.AddItem;

public record AddItemCommand(Guid ProductId, int Quantity) : IRequest<Guid>
{
    [JsonIgnore]
    public Guid OrderId { get; set; }
}

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, Guid>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public AddItemCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Guid> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);
        var productId = new ProductId(request.ProductId);

        var order = await _applicationDbContext.Orders
            .WithSpecification(new OrderByIdSpec(orderId))
            .FirstAsync(cancellationToken);

        var product = await _applicationDbContext.Products
            .WithSpecification(new ProductByIdSpec(productId))
            .FirstAsync(cancellationToken);

        var item = order.AddItem(product, request.Quantity);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return item.Id.Value;
    }
}
