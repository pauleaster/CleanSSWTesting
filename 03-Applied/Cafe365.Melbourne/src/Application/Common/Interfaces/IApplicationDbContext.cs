using Microsoft.EntityFrameworkCore;
using Cafe365.Melbourne.Domain.TodoItems;

namespace Cafe365.Melbourne.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoItem> TodoItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}