using Application.Features.Products.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SpaghettiCommerce.Data;

namespace Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product?> GetProduct(int id)
    {
        return await _context.CatalogProducts.FindAsync(id);
    }

    public async Task<List<Product>> SearchProducts(string searchTerm)
    {
        return await _context.CatalogProducts
            .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
            .ToListAsync();
    }
}