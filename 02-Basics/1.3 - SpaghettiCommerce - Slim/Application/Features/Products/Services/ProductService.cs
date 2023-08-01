using Application.Features.Products.Abstractions;
using Application.Features.Products.DTOs;
using Application.Features.Products.Mappings;
using Domain.Models;

namespace Application.Features.Products.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> GetProduct(int id)
    {
        var output = await _productRepository.GetProduct(id);
        
        
        
        return output?.ToProductDto();
    }

    public async Task<List<ProductDto>> SearchProducts(string searchTerm)
    {
        var products = await _productRepository.SearchProducts(searchTerm);

        return products.Select(p => p.ToProductDto()).ToList();
    }
}