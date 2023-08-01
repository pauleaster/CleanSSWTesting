using Microsoft.EntityFrameworkCore;
using Cafe365.Melbourne.Application.Common.Interfaces;
using Cafe365.Melbourne.Domain.TodoItems;
using Cafe365.Melbourne.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace Cafe365.Melbourne.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;
    private readonly DispatchDomainEventsInterceptor _dispatchDomainEventsInterceptor;

    public ApplicationDbContext(
        DbContextOptions options,
        EntitySaveChangesInterceptor saveChangesInterceptor,
        DispatchDomainEventsInterceptor dispatchDomainEventsInterceptor)
        : base(options)
    {
        _saveChangesInterceptor = saveChangesInterceptor;
        _dispatchDomainEventsInterceptor = dispatchDomainEventsInterceptor;
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Order of the interceptors is important
        optionsBuilder.AddInterceptors(_saveChangesInterceptor, _dispatchDomainEventsInterceptor);
    }
}