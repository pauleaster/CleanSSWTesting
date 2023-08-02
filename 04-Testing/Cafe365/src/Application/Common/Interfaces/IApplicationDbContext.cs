using Cafe365.Domain.Customers;
using Cafe365.Domain.Orders;
using Cafe365.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Cafe365.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Customer> Customers { get; }

    public DbSet<Order> Orders { get; }

    public DbSet<Product> Products { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
