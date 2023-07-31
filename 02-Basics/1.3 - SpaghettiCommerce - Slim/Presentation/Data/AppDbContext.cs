using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SpaghettiCommerce.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> CatalogProducts { get; set; }

    //public DbSet<Product> CatalogProducts { get; set; }
}
