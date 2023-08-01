using Domain.Models;

namespace Application.Features.Products.Abstractions;

public interface IProductRepository
{
    Task<Product?> GetProduct(int id);
    Task<List<Product>> SearchProducts(string searchTerm);
}