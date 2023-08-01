using Microsoft.EntityFrameworkCore;
using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Application.Features.TodoItems.Specifications;

namespace Cafe365.Melbourne.Application.Features.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateTodoItemCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle).WithMessage("'{PropertyName}' must be unique");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        var spec = new TodoItemByTitleSpec(title);

        var exists = await _dbContext.TodoItems
            .WithSpecification(spec)
            .AnyAsync(cancellationToken: cancellationToken);

        return !exists;
    }
}