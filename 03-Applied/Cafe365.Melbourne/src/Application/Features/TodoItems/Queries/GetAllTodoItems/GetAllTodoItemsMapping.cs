using Cafe365.Melbourne.Domain.TodoItems;

namespace Cafe365.Melbourne.Application.Features.TodoItems.Queries.GetAllTodoItems;

public class GetAllTodoItemsMapping : Profile
{
    public GetAllTodoItemsMapping()
    {
        CreateMap<TodoItem, TodoItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
    }
}