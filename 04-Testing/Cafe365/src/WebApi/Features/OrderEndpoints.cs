using Cafe365.Application.Features.Orders.Commands.AddItem;
using Cafe365.Application.Features.Orders.Commands.CreateOrder;
using Cafe365.Application.Features.Orders.Commands.SubmitOrder;
using Cafe365.Application.Features.Orders.Queries.GetOrder;
using Cafe365.Application.Features.Orders.Queries.GetOrders;
using Cafe365.WebApi.Extensions;
using MediatR;

namespace Cafe365.WebApi.Features;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("orders")
            .WithTags("Orders")
            .WithOpenApi();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetOrdersQuery(), ct))
            .WithName("GetOrders")
            .ProducesGet<Application.Features.Orders.Queries.GetOrders.OrderDto>();

        group
            .MapGet("/{orderId:guid}", (Guid orderId, ISender sender, CancellationToken ct) => sender.Send(new GetOrderQuery(orderId), ct))
            .WithName("GetOrder")
            .ProducesGet<Application.Features.Orders.Queries.GetOrder.OrderDto>();

        group
            .MapPost("/", async (CreateOrderCommand command, ISender sender, CancellationToken ct) =>
            {
                var orderId = await sender.Send(command, ct);
                return Results.CreatedAtRoute("GetOrder", new { orderId });
            })
            .WithName("CreateOrder")
            .ProducesPost();

        group
            .MapPost("/{orderId:guid}/Items", async (Guid orderId, AddItemCommand command, ISender sender, CancellationToken ct) =>
            {
                command.OrderId = orderId;
                await sender.Send(command, ct);
                return Results.CreatedAtRoute("GetOrder", new { orderId });
            })
            .WithName("AddItem")
            .ProducesPost();

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