using Cafe365.Melbourne.Domain.Common.Base;

namespace Cafe365.Melbourne.Domain.TodoItems;

public record TodoItemCompletedEvent(TodoItem Item) : DomainEvent;
