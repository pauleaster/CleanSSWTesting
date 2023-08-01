using Ardalis.Specification;
using Cafe365.Melbourne.Domain.TodoItems;

namespace Cafe365.Melbourne.Application.Features.TodoItems.Specifications;

public sealed class TodoItemByTitleSpec : Specification<TodoItem>
{
    public TodoItemByTitleSpec(string title)
    {
        Query.Where(i => i.Title == title);
    }
}