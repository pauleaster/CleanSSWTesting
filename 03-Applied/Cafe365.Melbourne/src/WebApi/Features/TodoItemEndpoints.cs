﻿using MediatR;
using Cafe365.Melbourne.Application.Features.TodoItems.Commands.CreateTodoItem;
using Cafe365.Melbourne.Application.Features.TodoItems.Queries.GetAllTodoItems;
using Cafe365.Melbourne.WebApi.Extensions;

namespace Cafe365.Melbourne.WebApi.Features;

public static class TodoItemEndpoints
{
    public static void MapTodoItemEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("todoitems")
            .WithTags("TodoItems")
            .WithOpenApi();

        group
            .MapGet("/", (ISender sender, CancellationToken ct)
                => sender.Send(new GetAllTodoItemsQuery(), ct))
            .WithName("GetTodoItems")
            .ProducesGet<TodoItemDto[]>();

        // TODO: Investigate examples for swagger docs. i.e. better docs than:
        // myWeirdField: "string" vs myWeirdField: "this-silly-string"
        // (https://github.com/SSWConsulting/Cafe365.Melbourne/issues/79)

        group
            .MapPost("/", (ISender sender, CreateTodoItemCommand command, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateTodoItem")
            .ProducesPost();
    }
}