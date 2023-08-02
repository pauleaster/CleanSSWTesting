using Cafe365.Application.Features.Orders.Commands.AddItem;
using Cafe365.Application.Features.Orders.Commands.CreateOrder;
using Cafe365.Application.Features.Orders.Commands.SubmitOrder;
using Cafe365.Application.Features.Orders.Queries.GetOrders;
using HotChocolate.Language;
using MediatR;

namespace GraphQL.Mutations;

public class Mutation
{
    public async Task<Guid> CreateOrder([Service(ServiceKind.Synchronized)] ISender sender, CreateOrderCommand command) => await sender.Send(command);

    public async Task<Guid> AddItem([Service(ServiceKind.Synchronized)] ISender sender, AddItemCommand command) => await sender.Send(command);

    public async Task<Guid> SubmitOrder([Service(ServiceKind.Synchronized)] ISender sender, SubmitOrderCommand command) => await sender.Send(command);
}
