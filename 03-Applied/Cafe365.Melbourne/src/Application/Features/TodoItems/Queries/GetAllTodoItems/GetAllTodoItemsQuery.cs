using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Application.Features.TodoItems.Specifications;

namespace Cafe365.Melbourne.Application.Features.TodoItems.Queries.GetAllTodoItems;

public record GetAllTodoItemsQuery : IRequest<IReadOnlyList<TodoItemDto>>;

public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, IReadOnlyList<TodoItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;

    public GetAllTodoItemsQueryHandler(
        IMapper mapper,
        IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<TodoItemDto>> Handle(
        GetAllTodoItemsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new AllTodoItemSpec();

        return await _dbContext.TodoItems
            .WithSpecification(spec)
            .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}