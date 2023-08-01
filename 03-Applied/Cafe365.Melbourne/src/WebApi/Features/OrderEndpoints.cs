using Cafe365.Melbourne.Application.Features.Orders.Commands.SubmitOrder;
using Cafe365.Melbourne.Application.Features.Orders.Queries.GetOrder;
using Cafe365.Melbourne.WebApi.Extensions;
using MediatR;

namespace Cafe365.Melbourne.WebApi.Features;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("orders")
            .WithTags("Orders")
            .WithOpenApi();

        group
            .MapGet("/{orderId:guid}", (Guid orderId, ISender sender, CancellationToken ct) => sender.Send(new GetOrderQuery(orderId), ct))
            .WithName("GetOrder")
            .ProducesGet<Application.Features.Orders.Queries.GetOrder.OrderDto>();

        group
            .MapPost("/{orderId:guid}/Submit", async (Guid orderId, SubmitOrderCommand command, ISender sender, CancellationToken ct) =>
            {
                command.OrderId = orderId;
                await sender.Send(command, ct);
                return Results.CreatedAtRoute("GetOrder", new { orderId });
            })
            .WithName("SubmitOrder")
            .ProducesPost();
    }
}
