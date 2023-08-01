using Cafe365.Melbourne.Domain.Common.Base;

namespace Cafe365.Melbourne.Domain.TodoItems;

public record TodoItemCreatedEvent(TodoItem Item) : DomainEvent;
