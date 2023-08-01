using Microsoft.EntityFrameworkCore;
using Cafe365.Melbourne.Domain.TodoItems;
using Cafe365.Melbourne.Domain.Customers;
using Cafe365.Melbourne.Domain.Orders;
using Cafe365.Melbourne.Domain.Products;

namespace Cafe365.Melbourne.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }

    DbSet<Product> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}