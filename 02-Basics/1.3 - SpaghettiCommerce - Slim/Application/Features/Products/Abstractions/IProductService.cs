using Application.Features.Products.DTOs;
using Domain.Models;

namespace Application.Features.Products.Abstractions;

public interface IProductService
{
    Task<ProductDto> GetProduct(int id);
    
    Task<List<ProductDto>> SearchProducts( string searchTerm);
}